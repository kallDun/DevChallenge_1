using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using System.Collections.Generic;

namespace SC.DevChallenge.Api.Repository
{
    public interface IPortfolioRepository : IReadonlyRepository<Portfolio>
    {
        IEnumerable<Portfolio> GetSelection(AveragePricePortfolioDto dto);

        IEnumerable<Portfolio> GetSelection(BenchmarkPortfolioDto dto);

        IEnumerable<AggregatePortfolioReturnDto> GetAggregateSelections(AggregatePortfolioDto dto);

        double GetAverageCostBySelection(IEnumerable<Portfolio> selection);

    }
}
