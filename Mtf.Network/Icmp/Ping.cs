using System;
using System.Net.NetworkInformation;
using System.Threading;
using Mtf.Utils.StringExtensions;

namespace Mtf.Network.Icmp
{
    public sealed class Ping : IDisposable
    {
        public delegate void PingReplyArrivedEventHandler(object sender, PingReplyArrivedEventArgs e);

        public event PingReplyArrivedEventHandler PingReplyArrived;

        private int cleanup;
        private readonly System.Net.NetworkInformation.Ping ping;

        public Ping(byte[] ipAddress, int timeout = 10, string data = null, PingReplyArrivedEventHandler PingReplyArrivedHandler = null)
            : this(String.Join(".", ipAddress), timeout, data, PingReplyArrivedHandler)
        {
        }

        public Ping(string ipAddress, int timeout = 10, string data = null, PingReplyArrivedEventHandler PingReplyArrivedHandler = null)
        {
            if (PingReplyArrivedHandler != null)
            {
                PingReplyArrived += PingReplyArrivedHandler;
            }
            try
            {
                ping = new System.Net.NetworkInformation.Ping();
                ping.PingCompleted += PingResult;
                var dataToSend = String.IsNullOrEmpty(data) ? "ping".ToByteArray() : data.ToByteArray();
                ping.SendAsync(ipAddress, timeout, dataToSend, null);
            }
            catch
            {
                Dispose();
            }
        }

        ~Ping()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Interlocked.Exchange(ref cleanup, 1) != 0)
            {
                return;
            }
            ((IDisposable)ping)?.Dispose();
        }

        public static PingReplyMessage PingDiagnosticResult(PingReplyArrivedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            if (e.PingReply == null || e.PingReply.Status != IPStatus.Success)
            {
                return new PingReplyMessage(false, "Ping failed", e.StatusMessage);
            }

            return new PingReplyMessage(true, "Ping successful", $"Ping reply arrived in {e.PingReply.RoundtripTime} ms");
        }

        private void PingResult(object sender, PingCompletedEventArgs e)
        {
            var pingReplyArrivedEventArgs = new PingReplyArrivedEventArgs(e.Reply, e.Cancelled, e.Error);
            PingReplyArrived?.Invoke(this, pingReplyArrivedEventArgs);
            ((IDisposable)ping).Dispose();
        }
    }
}