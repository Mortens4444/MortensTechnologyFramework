using System;
using System.Collections.Generic;
using System.Text;

namespace Mtf.Log
{
    public class LogInfo : LogInfoBase
    {
        protected LogInfo(string logger, string message,
            LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
            : this(logger, message, DateTimeOffset.UtcNow, logSeverity, logDetails)
        { }

        protected LogInfo(string logger, string message,
            DateTimeOffset eventTime,
            LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
            : base(eventTime, logSeverity, logDetails)
        {
            Logger = logger;
            LogSeverity = logSeverity;
            EventTime = eventTime;
            Message = message;
            LogDetails = logDetails;
        }

        public string Logger { get; set; }

        public string Message { get; }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());

            if (String.IsNullOrEmpty(Logger))
            {
                sb.AppendLine($"Logger: {Logger}");
            }
            if (String.IsNullOrEmpty(Message))
            {
                sb.AppendLine($"Message: {Message}");
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}