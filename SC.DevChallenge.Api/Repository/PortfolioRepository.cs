using SC.DevChallenge.Api.Data;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private DataContext dataContext;
        public PortfolioRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Portfolio> GetAll() => dataContext.Portfolios;


        public Portfolio GetById(Guid Id) => dataContext.Portfolios.FirstOrDefault(x => x.Id == Id);


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

        public double GetAverageBySelection(IEnumerable<Portfolio> selection) => selection.Average(x => x.Price);
    }
}
