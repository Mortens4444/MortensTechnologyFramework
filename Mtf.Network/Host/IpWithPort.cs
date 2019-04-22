namespace Mtf.Network.Host
{
    public class IpWithPort
    {
        public string IpAddress { get; }

        public string Port { get; }

        public IpWithPort(string ipAddress, string port)
        {
            IpAddress = ipAddress;
            Port = port;
        }
    }
}