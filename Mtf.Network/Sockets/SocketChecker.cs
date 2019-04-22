using System.Net.Sockets;

namespace Mtf.Network.Sockets
{
    public class SocketChecker
    {
        public bool IsSocketConnected(Socket socket)
        {
            var result = false;
            if (socket.Connected)
            {
                try
                {
                    socket.Send(new byte[] { 0 }, 1, SocketFlags.None);
                }
                catch { }
                result = socket.Connected;
            }
            return result;
        }
    }
}