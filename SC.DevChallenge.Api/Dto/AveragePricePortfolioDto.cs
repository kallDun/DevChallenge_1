using SC.DevChallenge.Api.Models;

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
