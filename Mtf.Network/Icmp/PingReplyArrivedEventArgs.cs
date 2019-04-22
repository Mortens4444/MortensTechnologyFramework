using System;
using System.Net.NetworkInformation;
using Mtf.Reflection.ExceptionInfo;

namespace Mtf.Network.Icmp
{
    public class PingReplyArrivedEventArgs : EventArgs
    {
        public string Sender { get; }

        public PingReply PingReply { get; }

        public bool Success { get; }

        public string StatusMessage { get; }

        public bool Cancelled { get; }

        public Exception Error { get; }

        public PingReplyArrivedEventArgs(PingReply pingReply, bool cancelled, Exception error)
        {
            PingReply = pingReply;
            Cancelled = cancelled;
            Error = error;
            if (pingReply?.Address != null)
            {
                Sender = pingReply.Address.ToString();
                Success = pingReply.Status == IPStatus.Success;
                StatusMessage = GetIpStatusDescription(pingReply.Status);
            }
            else
            {
                Sender = null;
                Success = false;
                StatusMessage = error != null ? new ExceptionDetails(error).Details :
                    cancelled ? "Ping request has been cancelled." : GetIpStatusDescription(IPStatus.Unknown);
            }
        }

        public override string ToString()
        {
            return StatusMessage;
        }

        private static string GetIpStatusDescription(IPStatus status)
        {
            var ipStatusDescriptionProvider = new IpStatusDescriptionProvider();
            return ipStatusDescriptionProvider.GetDescription(status);
        }
    }
}