using System.Diagnostics;
using System.Net;

namespace Mtf.Log
{
    public static class EventLogUtils
    {
        public static void ClearEventLog(EventLogType type)
        {
            var eventLog = new EventLog(type.ToString());
            eventLog.Clear();
        }

        public static EventLogInfo[] GetEventLog(EventLogType type)
        {
            var e = new EventLog(type.ToString());
            var logs = new EventLogInfo[e.Entries.Count];
            var i = 0;
            while (i < e.Entries.Count)
            {
                logs[i] = new EventLogInfo(e.Entries[i]);
                i++;
            }
            return logs;
        }

        public static void WriteEventLog(EventLogType type, LogInfo info, string source)
        {
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, type.ToString());
            }
            var eventLog = new EventLog(type.ToString(), Dns.GetHostName(), source);
            eventLog.WriteEntry(info.ToString());
        }
    }
}