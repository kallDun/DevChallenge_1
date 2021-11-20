using SC.DevChallenge.Api.Service;
using System;

namespace SC.DevChallenge.Api.Models
{
    public class TimeSlot : IComparable<TimeSlot>
    {
        public int Count { get; set; }

        public DateTime IntervalStart { get; set; }

        public TimeSlot(DateTime date)
        {
            TimeSlotService service = new();
            (Count, IntervalStart) = service.GetTimeSlotStartFromDate(date);
        }

        public int CompareTo(TimeSlot other)
        {
            return IntervalStart.CompareTo(other.IntervalStart);
        }
    }
}
