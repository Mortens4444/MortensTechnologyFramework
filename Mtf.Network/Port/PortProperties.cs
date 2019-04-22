using System;
using System.Text;

namespace Mtf.Network.Port
{
    public class PortProperties
    {
        public int PortNumber { get; }

        public Protocol Protocol { get; }

        public string ServiceName { get; }

        public string ServiceAlias { get; }

        public string ServiceDescription { get; }

        public string KnownAttacks { get; }

        public PortProperties(int portNumber, Protocol protocol, string serviceName, string serviceAlias = "", string serviceDescription = "", string knownAttacks = "")
        {
            PortNumber = portNumber;
            Protocol = protocol;
            ServiceName = serviceName;
            ServiceAlias = serviceAlias;
            ServiceDescription = serviceDescription;
            KnownAttacks = knownAttacks;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append($"{PortNumber}");

            if (ServiceName != String.Empty)
            {
                toString.Append($" {ServiceName}");
            }
            if (ServiceAlias != String.Empty)
            {
                toString.Append($" [{ServiceAlias}]");
            }
            if (ServiceDescription != String.Empty)
            {
                toString.Append($" - {ServiceDescription}");
            }
            return base.ToString();
        }
    }
}
