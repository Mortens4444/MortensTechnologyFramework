using System.Net.Sockets;

namespace Mtf.Core.Network.Sockets
{
    public interface ISocketCloser
    {
        void Close(Socket socket);

        /// <summary>
        /// This method requires Windows 2000 or earlier.
        /// </summary>
        /// <param name="socket"></param>
        void CloseWindows2000OrOlder(Socket socket);
    }
}