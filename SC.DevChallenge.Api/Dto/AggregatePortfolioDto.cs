using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Dto
{
    public class AggregatePortfolioDto
    {
        public string Name { get; set; }

        public TimeSlot StartDate { get; set; }

        public TimeSlot EndDate { get; set; }

        public int GroupsCount { get; set; }
    }
}
