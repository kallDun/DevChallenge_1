using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Dto
{
    public class AggregatePortfolioReturnDto
    {
        public List<IEnumerable<Portfolio>> Portfolios { get; set; }

        public DateTime IntervalStart { get; set; }
    }
}
