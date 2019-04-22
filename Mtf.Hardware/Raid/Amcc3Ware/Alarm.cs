using System;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class Alarm
    {
        public DateTime? AlarmTime;
        public string Severity;
        public string Message;

        public Alarm(DateTime? alarm_time, string severity, string message)
        {
            AlarmTime = alarm_time;
            Severity = severity;
            Message = message;
        }

        public override string ToString()
        {
            if (AlarmTime == null)
            {
                return Message;
            }

            return $"{AlarmTime.Value.ToShortDateString()} {AlarmTime.Value.ToLongTimeString()}\t{Severity}: {Message}";
        }
    }
}