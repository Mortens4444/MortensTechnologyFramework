using System;

namespace Mtf.Utils.Int64Extensions
{
    public static class Time
    {
        public static TimeSpan ToTimeSpan(this long value)
        {
            var ms = (int)value % 100;
            var seconds = value / 100;

            var minutes = seconds / 60;
            seconds -= minutes * 60;

            var hours = (int)(minutes / 60);
            minutes -= hours * 60;

            var days = hours / 24;
            hours -= days * 24;

            return new TimeSpan(days, hours, (int)minutes, (int)seconds, ms);
        }
    }
}