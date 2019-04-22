using System;
using System.Collections.Generic;
using System.Net;

namespace Mtf.Network.Host
{
    public class HostnameProvider
    {
        private delegate IPHostEntry GetHostNameCallback(string hostname);
        private readonly IEnumerable<GetHostNameCallback> GetHostNameCallbacks = new List<GetHostNameCallback> { Dns.GetHostEntry, Dns.GetHostByAddress };

        public string GetHostName(string ipAddress)
        {
            var result = ipAddress;
            if (ipAddress != String.Empty)
            {
                foreach (var getHostNameCallback in GetHostNameCallbacks)
                {
                    #pragma warning disable 618
                    var hostInfoResult = GetHostName(getHostNameCallback, ipAddress);
                    #pragma warning restore 618
                    if (hostInfoResult.Success)
                    {
                        result = hostInfoResult.HostInfo.HostName;
                        break;
                    }
                }
            }
            return result;
        }

        private static GetHostInfoResult GetHostName(GetHostNameCallback callback, string ipAddress)
        {
            try
            {
                return new GetHostInfoResult
                {
                    HostInfo = callback(ipAddress),
                    Success = true
                };
            }
            catch
            {
                return new GetHostInfoResult { Success = false };
            }
        }
    }

    internal class GetHostInfoResult
    {
        public IPHostEntry HostInfo { get; set; }

        public bool Success { get; set; }
    }
}