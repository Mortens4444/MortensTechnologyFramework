namespace Mtf.Network.Host
{
    public class IpAddressSetting
    {
        public string IpAddress { get; set; }

        public string SubnetMask { get; set; }

        public IpAddressSetting(string ipAddress, string subnetMask)
        {
            IpAddress = ipAddress;
            SubnetMask = subnetMask;
        }
    }
}