using SC.DevChallenge.Api.Data;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using SC.DevChallenge.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SC.DevChallenge.Api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private AbstractDataContext dataContext;
        public PortfolioRepository(AbstractDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Portfolio> GetAll() => dataContext.Portfolios;

        public Portfolio GetById(Guid Id) => dataContext.Portfolios.FirstOrDefault(x => x.Id == Id);


        public double GetAverageCostBySelection(IEnumerable<Portfolio> selection) => selection.Average(x => x.Price);

        public IEnumerable<Portfolio> GetSelection(AveragePricePortfolioDto dto)
        {
            IEnumerable<Portfolio> selection = dataContext.Portfolios;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                selection = selection.Where(x => x.Name == dto.Name);
            }
            if (!string.IsNullOrEmpty(dto.InstrumentOwner))
            {
                selection = selection.Where(x => x.InstrumentOwner == dto.InstrumentOwner);
            }
            if (!string.IsNullOrEmpty(dto.Instrument))
            {
                selection = selection.Where(x => x.Instrument == dto.Instrument);
            }
            if (dto.Date is not null)
            {
                selection = selection.Where(x => x.Date.CompareTo(dto.Date) == 0);
            }

            return selection;
        }

        public IEnumerable<Portfolio> GetSelection(BenchmarkPortfolioDto dto)
        {
            IEnumerable<Portfolio> timeslot_selection = dataContext.Portfolios.Where(x => x.Name == dto.Name && x.Date.CompareTo(dto.Date) == 0);
            if (!timeslot_selection.Any()) return new List<Portfolio>();
            return FindQuartilesSelection(timeslot_selection);
        }

        private static IEnumerable<Portfolio> FindQuartilesSelection(IEnumerable<Portfolio> input_selection)
        {
            var sorted = input_selection.OrderBy(x => x.Price).ToArray();
            var n = sorted.Length;
            if (n is 0) return new List<Portfolio>();

            var Q1 = Math.Ceiling((n - 1) / 4.0);
            var Q3 = Math.Ceiling((3 * n - 3) / 4.0);
            Q1 = sorted[(int)Q1].Price;
            Q3 = sorted[(int)Q3].Price;

            var IQR = Q3 - Q1;
            var start = Q1 - 1.5 * IQR;
            var end = Q3 + 1.5 * IQR;

            return sorted.Where(x => x.Price >= start && x.Price <= end);
        }

        public IEnumerable<AggregatePortfolioReturnDto> GetAggregateSelections(AggregatePortfolioDto dto)
        {
            List<Portfolio> timeslot_selection = dataContext.Portfolios
                .Where(x => x.Name == dto.Name)
                .Where(x => x.Date.IntervalStart > dto.StartDate.IntervalStart && x.Date.IntervalStart < dto.EndDate.IntervalStart)
                .ToList();
            if (!timeslot_selection.Any()) return new List<AggregatePortfolioReturnDto>();

            TimeSlotService service = new();
            var different_timeslots = service.GetTimeSlotsBetween(dto.StartDate.IntervalStart, dto.EndDate.IntervalStart);
            var timeslots_count = different_timeslots.Count();

            List<int> groups = new();
            var whole = timeslots_count / dto.GroupsCount;
            var remainder = timeslots_count % dto.GroupsCount;
            for (int i = 0; i < dto.GroupsCount; i++)
            {
                if (remainder > 0)
                {
                    groups.Add(whole + 1);
                    remainder--;
                }
                else groups.Add(whole);
            }

            var aggregateSelections = new List<AggregatePortfolioReturnDto>();
            var timeslot_index = 0;
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i] == 0) break;

                var result_dto = new AggregatePortfolioReturnDto { IntervalStart = different_timeslots[timeslot_index], Portfolios = new List<IEnumerable<Portfolio>>() };

                for (int j = 0; j < groups[i]; j++)
                {
                    var selection__ = FindQuartilesSelection(timeslot_selection.Where(x => x.Date.IntervalStart == different_timeslots[timeslot_index]));
                    result_dto.Portfolios.Add(selection__);
                    timeslot_index++;
                }

                aggregateSelections.Add(result_dto);
            }
            return aggregateSelections;
        }
    }
}
