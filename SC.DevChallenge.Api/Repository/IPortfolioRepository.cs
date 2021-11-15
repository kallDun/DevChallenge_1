using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Repository
{
    interface IPortfolioRepository : IReadonlyRepository<Portfolio>
    {
        IEnumerable<Portfolio> GetSelection(AveragePricePortfolioDto dto);

        double GetAverageBySelection(IEnumerable<Portfolio> selection);
    }
}
