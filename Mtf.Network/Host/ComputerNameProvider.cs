using System;
using System.Net;
using System.Windows.Forms;

namespace Mtf.Network.Host
{
    public class ComputerNameProvider
    {
        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        public string GetComputerName()
        {
            return SystemInformation.ComputerName;
        }

        public string GetHostName()
        {
            return Dns.GetHostName();
        }

        public string GetEnvironmentVariableComputerName()
        {
            return Environment.GetEnvironmentVariable("ComputerName");
        }
    }
}