using System;
using System.Diagnostics;
using System.Text;

namespace Mtf.Utils.DateExtensions
{
    public class UptimeMeasure
    {
        private readonly uint startTick;
        private uint lastTick;
        private double wraps;

        public UptimeMeasure()
        {
            startTick = (uint)Environment.TickCount;
            lastTick = startTick;
            wraps = 0;
        }

        public DateTime GetStartTime()
        {
            DateTime result;
            using (var process = Process.GetCurrentProcess())
            {
                result = process.StartTime;
                process.Close();
            }
            return result;
        }

        public TimeSpan GetUptime()
        {
            var nowTick = (uint)Environment.TickCount;

            if (nowTick < lastTick)
            {
                wraps++;
            }
            lastTick = nowTick;
            var add = nowTick > startTick ? nowTick - startTick : UInt64.MaxValue - startTick + nowTick;
            var totalMilliseconds = wraps * UInt64.MaxValue + add;
            return TimeSpan.FromMilliseconds(totalMilliseconds);
        }

        public string GetUptimeString()
        {
            var timeSpan = GetUptime();
            var uptime = new StringBuilder("Uptime: ");
            var daysString = timeSpan.Days > 1 ? "days" : "day";
            uptime.Append($"{timeSpan.Days} {daysString} {timeSpan.Hours}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}");
            return uptime.ToString();
        }
    }
}