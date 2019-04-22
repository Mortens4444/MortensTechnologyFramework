using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Mtf.Utils.EnumExtensions;

namespace Mtf.Network
{
    public class NetworkStatistics
    {
        public NetworkStatistics(ProtocolType protocol, int state, IPEndPoint local, IPEndPoint remote, int processId)
        {
            Protocol = protocol;
            State = state;
            LocalIp = local;
            RemoteIp = remote;
            ProcessId = processId;
            try
            {
                var ps = Process.GetProcessById(processId);
                ProcessName = ps.ProcessName;
                ProcessFilename = ps.MainModule.FileName;
            }
            catch { }
        }

        public ProtocolType Protocol { get; }

        public IPEndPoint LocalIp { get; }

        public int State { get; }

        public string StateString => State.GetDescription();

        public IPEndPoint RemoteIp { get; }

        public int ProcessId { get; }

        public string ProcessName { get; }

        public string ProcessFilename { get; }

        public ListViewItem ToListViewItem()
        {
            var item = new ListViewItem(ProcessName)
            {
                Tag = ProcessId
            };
            item.SubItems.Add(Protocol.ToString());
            item.SubItems.Add(LocalIp == null ? "N/A" : $"{LocalIp.Address}:{LocalIp.Port}");
            item.SubItems.Add(RemoteIp == null ? "N/A" : $"{RemoteIp.Address}:{RemoteIp.Port}");
            return item;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(LocalIp.ToString());
            if (RemoteIp != null)
            {
                sb.AppendLine(RemoteIp.ToString());
            }
            if (!String.IsNullOrEmpty(ProcessName))
            {
                sb.AppendLine($"{ProcessName} ({ProcessId})");
                if (ProcessFilename != null)
                {
                    sb.AppendLine(ProcessFilename);
                }
            }
            else
            {
                sb.AppendLine("Process not available");
            }
            sb.AppendLine();
            return sb.ToString();
        }
    }
}