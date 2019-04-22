using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Mtf.Log
{
    public class EventLogInfo : LogInfoBase
    {
        public EventLogEntry EventLogEntry { get; }

        public EventLogInfo(EventLogEntry eventLogEntry,
            LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
            : this(DateTimeOffset.UtcNow, eventLogEntry, logSeverity, logDetails)
        { }

        public EventLogInfo(DateTimeOffset eventTime,
            EventLogEntry eventLogEntry,
            LogSeverity logSeverity = LogSeverity.Information,
            params KeyValuePair<string, object>[] logDetails)
            : base(eventTime, logSeverity, logDetails)
        {
            EventLogEntry = eventLogEntry;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("________________________________________________________________________________");
            sb.AppendLine();
            sb.AppendLine($"Event time: {EventLogEntry.TimeWritten.ToShortDateString()} {EventLogEntry.TimeWritten.ToLongTimeString()}");
            sb.AppendLine($"InstanceId: {EventLogEntry.InstanceId}");
            sb.AppendLine($"Category: {EventLogEntry.Category}");
            sb.AppendLine($"Message: {EventLogEntry.Message}");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}