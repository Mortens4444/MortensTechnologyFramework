using System;

namespace Mtf.Network.Port
{
    public class FreePortProvider
    {
        private readonly PortExaminer portExaminer;

        public FreePortProvider()
        {
            portExaminer = new PortExaminer();
        }

        /// <summary>
        /// Gets a random free port.
        /// </summary>
        /// <returns>Number of the port.</returns>
        public int GetFreePort()
        {
            var rnd = new Random(Environment.TickCount);
            int port;

            do
            {
                port = rnd.Next(1024, 65535);
            }
            while (!portExaminer.IsLocalPortAvailable(port));

            return port;
        }
    }
}