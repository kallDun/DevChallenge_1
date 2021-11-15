using Microsoft.AspNetCore.Mvc;
using SC.DevChallenge.Api.Data;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using SC.DevChallenge.Api.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public ActionResult<AveragePriceReturnDto> Average(string portfolio, string owner, string instrument, string date)
        {
            try
            {
                var datetime = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("fr-FR"));
                TimeSlot timeSlot = new TimeSlot(datetime);
                var dto = new AveragePricePortfolioDto { Name = portfolio, InstrumentOwner = owner, Instrument = instrument, Date = timeSlot };
                
                var selection = portfolioRepository.GetSelection(dto);
                if (selection.Any())
                {
                    return new AveragePriceReturnDto
                    {
                        Price = string.Format("{0:0.00}", portfolioRepository.GetAverageBySelection(selection)),
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

    }
}
