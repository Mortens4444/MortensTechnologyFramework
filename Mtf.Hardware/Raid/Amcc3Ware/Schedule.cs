using System;
using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.DateExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Schedule
    {
        public int Slot;
        public Day? Day;
        public TimeSpan? Hour;
        public TimeSpan? Duration;
        public string Status;

        public Schedule(int slot, Day? day, TimeSpan? hour, TimeSpan? duration, string status)
        {
            Day = day;
            Duration = duration;
            Hour = hour;
            Slot = slot;
            Status = status;
        }

        public override string ToString()
        {
            if (Day.HasValue && Hour.HasValue)
            {
                return $"On {Day.Value} at {Hour.Value.TotalHours} {Status}";
            }

            return Status;
        }
    }
}