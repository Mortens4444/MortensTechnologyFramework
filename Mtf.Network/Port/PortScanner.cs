using System.Net.Sockets;

namespace Mtf.Network.Port
{
    public class PortScanner
    {
        private readonly PortInfoProvider portInfoProvider;

        public PortScanner()
        {
            portInfoProvider = new PortInfoProvider();
        }

        private PortProperties ConnectToPort(PortConnector portConnector)
        {
            try
            {
                portConnector.Socket.Connect(portConnector.HostnameOrIp, portConnector.PortNumber);

                /*System.IAsyncResult res = port_connector.Socket.BeginConnect(port_connector.HostnameOrIP, port_connector.PortNumber, null, null);
                bool success = res.AsyncWaitHandle.WaitOne(200, true);
                if ((success) && (port_connector.Socket.Connected))*/

                if (portConnector.Socket.Connected)
                {
                    return portInfoProvider.GetPortProperties(portConnector.PortNumber);
                }
            }
            catch { }
            /*catch (SocketException ex)
            {
                ex.ErrorCode;
            }*/
            finally
            {
                if (portConnector.Socket != null)
                {
                    try { portConnector.Socket.Shutdown(SocketShutdown.Both); } catch { }
                    try { portConnector.Socket.Disconnect(false); } catch { }
                    try { portConnector.Socket.Close(); } catch { }
                }
            }
            return null;
        }
    }
}