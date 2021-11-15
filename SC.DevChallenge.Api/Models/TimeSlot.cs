using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Models
{
    public class TimeSlot : IComparable<TimeSlot>
    {
        public const int TimeInterval = 10;

        public DateTime IntervalStart { get; set; }

        public TimeSlot(DateTime date)
        {
            IntervalStart = date.AddSeconds(-(date.Second % 10));
        }

        public int CompareTo(TimeSlot other)
        {
            return IntervalStart.CompareTo(other);
        }
    }
}
