using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;

namespace SC.DevChallenge.Api.Service
{
    public class TimeSlotService
    {
        private static readonly DateTime startDate = new(2018, 01, 01);
        private static readonly int Interval = 10000;

        public List<DateTime> GetTimeSlotsBetween(DateTime start, DateTime end)
        {
            var different_timeslots = new List<DateTime>();

            var intervalStart = start;
            var next_intervalStart = intervalStart.AddSeconds(Interval);
            while (next_intervalStart < end)
            {
                different_timeslots.Add(intervalStart);
                intervalStart = next_intervalStart;
                next_intervalStart = next_intervalStart.AddSeconds(Interval);
            }
            different_timeslots.Add(intervalStart);

            return different_timeslots;
        }

        public (int, DateTime) GetTimeSlotStartFromDate(DateTime date)
        {
            var count = 0;
            var intervalStart = startDate;
            var next_intervalStart = intervalStart.AddSeconds(Interval);

            while (next_intervalStart < date)
            {
                intervalStart = next_intervalStart;
                next_intervalStart = next_intervalStart.AddSeconds(Interval);
                count++;
            }

            return (count, intervalStart);
        }

    }
}
