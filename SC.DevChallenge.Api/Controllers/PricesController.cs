using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Data;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using SC.DevChallenge.Api.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private IPortfolioRepository portfolioRepository;
        public PricesController()
        {
            portfolioRepository = new PortfolioRepository(DataContextSingleton.GetInstance());
        }


        [HttpGet("average")]
        public async Task<ActionResult<PortfolioReturnDto>> Average(string portfolio, string owner, string instrument, string date)
        {
            try
            {
                TimeSlot timeSlot = string.IsNullOrEmpty(date) 
                    ? null
                    : new TimeSlot(DateTime.Parse(date, CultureInfo.CreateSpecificCulture("fr-FR")));

                var dto = new AveragePricePortfolioDto { Name = portfolio, InstrumentOwner = owner, Instrument = instrument, Date = timeSlot };
                
                var selection = await Task.FromResult(portfolioRepository.GetSelection(dto));
                if (selection.Any())
                {
                    return new PortfolioReturnDto
                    {
                        Price = string.Format("{0:0.00}", portfolioRepository.GetAverageCostBySelection(selection)),
                        Date = timeSlot is null ? "null" : timeSlot.IntervalStart.ToString()
                    };
                }
                else return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("benchmark")]
        public async Task<ActionResult<PortfolioReturnDto>> Benchmark(string portfolio, string date)
        {
            try
            {
                TimeSlot timeSlot = new(DateTime.Parse(date, CultureInfo.CreateSpecificCulture("fr-FR")));
                var dto = new BenchmarkPortfolioDto { Name = portfolio, Date = timeSlot };

                var selection = await Task.FromResult(portfolioRepository.GetSelection(dto));
                if (selection.Any())
                {
                    return new PortfolioReturnDto
                    {
                        Price = string.Format("{0:0.00}", portfolioRepository.GetAverageCostBySelection(selection)),
                        Date = timeSlot.IntervalStart.ToString()
                    };
                }
                else return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("aggregate")]
        public async Task<ActionResult<IEnumerable<PortfolioReturnDto>>> Aggregate(string portfolio, string startdate, string enddate, int intervals)
        {
            try
            {
                TimeSlot start_timeslot = new(DateTime.Parse(startdate, CultureInfo.CreateSpecificCulture("fr-FR")));
                TimeSlot end_timeslot = new(DateTime.Parse(enddate, CultureInfo.CreateSpecificCulture("fr-FR")));
                if (start_timeslot.CompareTo(end_timeslot) == 1) return NotFound(); // if starttime more than endtime

                var dto = new AggregatePortfolioDto { Name = portfolio, StartDate = start_timeslot, EndDate = end_timeslot, GroupsCount = intervals };
                var selections = portfolioRepository.GetAggregateSelections(dto);

                if (selections.Where(x => x is not null && x.Portfolios.Any()).Any()) // if at least one selection exists
                {
                    var result_list = new List<PortfolioReturnDto>();
                    foreach (var selection in selections)
                    {
                        var average = await Task.FromResult(selection.Portfolios
                            .Where(x => x is not null && x.Any())
                            .Average(x => portfolioRepository.GetAverageCostBySelection(x)));

                        result_list.Add(selection.Portfolios.Any()
                            ? new PortfolioReturnDto { Date = selection.IntervalStart.ToString(), Price = string.Format("{0:0.00}", average) }
                            : new PortfolioReturnDto { Date = selection.IntervalStart.ToString(), Price = "Not Found" });
                    }
                    return result_list;
                }
                else return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}