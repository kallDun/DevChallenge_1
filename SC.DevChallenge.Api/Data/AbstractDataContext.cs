using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Data
{
    public abstract class AbstractDataContext
    {

        protected List<Portfolio> portfolios;

        public List<Portfolio> Portfolios => portfolios;
    }
}
