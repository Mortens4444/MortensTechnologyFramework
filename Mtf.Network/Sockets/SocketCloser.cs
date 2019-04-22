using System.Net.Sockets;
using Mtf.Core.Network.Sockets;

namespace Mtf.Network.Sockets
{
    public class SocketCloser : ISocketCloser
    {
        public void Close(Socket socket)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch { }
        }

        /// <summary>
        /// This method requires Windows 2000 or earlier.
        /// </summary>
        /// <param name="socket"></param>
        public void CloseWindows2000OrOlder(Socket socket)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                try
                {
                    socket.Disconnect(false);
                }
                catch { }
                socket.Close();
            }
            catch { }
        }
    }
}