using System;
using System.Collections.Generic;
using System.Text;

namespace Mtf.Log
{
    public abstract class LogInfoBase
    {
        protected LogInfoBase(LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
            : this(DateTimeOffset.UtcNow, logSeverity, logDetails)
        { }

        protected LogInfoBase(DateTimeOffset eventTime,
            LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
        {
            LogSeverity = logSeverity;
            EventTime = eventTime;
            LogDetails = logDetails;
        }

        public LogSeverity LogSeverity { get; protected set; }

        public DateTimeOffset EventTime { get; protected set; }

        public KeyValuePair<string, object>[] LogDetails { get; protected set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var eventTime = EventTime.LocalDateTime;

            sb.AppendFormat("________________________________________________________________________________");
            sb.AppendLine();

            sb.AppendLine($"Event time: {eventTime.ToShortDateString()} {eventTime.ToLongTimeString()}");
            sb.AppendLine($"Log severity: {LogSeverity}");

            foreach (var logDetail in LogDetails)
            {
                sb.AppendLine($"{logDetail.Key}: {logDetail.Value}");
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}