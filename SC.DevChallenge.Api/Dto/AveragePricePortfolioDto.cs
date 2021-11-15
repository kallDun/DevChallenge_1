using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Dto
{
    public class AveragePricePortfolioDto
    {
        public string Name { get; set; }

        public string InstrumentOwner { get; set; }

        public string Instrument { get; set; }

        public TimeSlot Date { get; set; }

    }
}
