using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mtf.Network.Port;
using Mtf.Network.Sockets;

namespace Mtf.Network.Client
{
    public abstract class ClientBase : IDisposable
    {
        private const int MaxPendingConnection = 10; // Maximum number of pending connections

        private int timeout;

        public const int MaxBufferSize = 8192;

        private readonly byte[] buffer;

        public delegate void DataArrivedEventHandler(object sender, DataArrivedEventArgs e);

        public event DataArrivedEventHandler DataArrived;

        public delegate void DisconnectionEventHandler(object sender, DisconnectionEventArgs e);

        public event DisconnectionEventHandler Disconnected;

        public string ServerHostnameOrIpAddress { get; }

        protected ClientBase(string serverHostnameOrIpAddress, DataArrivedEventHandler dataArrived, ushort listenerPortOfServer)
        {
            Encoding = Encoding.UTF8;
            Socket = null;
            ListenerPortOfServer = listenerPortOfServer;
            buffer = new byte[MaxBufferSize];
            ServerHostnameOrIpAddress = serverHostnameOrIpAddress;
            DataArrived = dataArrived;
        }

        ~ClientBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Socket == null)
            {
                return;
            }

            var socketChecker = new SocketChecker();
            if (socketChecker.IsSocketConnected(Socket))
            {
                var socketCloser = new SocketCloser();
                socketCloser.Close(Socket);
            }
            if (Socket.Connected)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public object Tag { get; set; }

        /// <summary>
        /// Encoding of the messages.
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Encoding of the messages.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Portnumber of the Server.
        /// </summary>
        public ushort ListenerPortOfServer { get; set; }

        public int TimeOut
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);
            }
        }

        public int SendBufferSize
        {
            get
            {
                return Socket.SendBufferSize;
            }
            set
            {
                Socket.SendBufferSize = value;
            }
        }

        public int ReceiveBufferSize
        {
            get
            {
                return Socket.ReceiveBufferSize;
            }
            set
            {
                Socket.ReceiveBufferSize = value;
            }
        }

        protected virtual void OnDataArrived(DataArrivedEventArgs e)
        {
            var handler = DataArrived;
            handler?.Invoke(this, e);
        }

        protected virtual void OnDisconnection(DisconnectionEventArgs e)
        {
            var handler = Disconnected;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Sends a string message to the Server. Using Client.Encoding which default value is UTF-8 encoding.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void Send(string message)
        {
            Send(Encoding.GetBytes(message));
        }

        /// <summary>
        /// Sends data, if socket is not connected, try to send to all clients.
        /// </summary>
        /// <param name="bytes">The byte array to send.</param>
        /// <returns>True, if all bytes has been sent</returns>
        public virtual bool Send(byte[] bytes)
        {
            var success = false;
            try
            {
#pragma warning disable 642
                if (Socket.RemoteEndPoint == null) ;
#pragma warning restore 642
                var count = 0;
                SocketException rex = null;

                do
                {
                    try
                    {
                        count++;

                        //Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, WaitResponse, Socket);
                        var sentBytes = Socket.Send(bytes, bytes.Length, SocketFlags.None);
                        success = sentBytes == bytes.Length;
                    }
                    catch (SocketException ex)
                    {
                        rex = ex;
                    }
                }
                while (!success && count <= 3);

                if (!success && rex != null)
                {
                    throw rex;
                }
            }
            catch (SocketException)
            {
                OnDisconnection(new DisconnectionEventArgs(this));
            }
            return success;
        }

        public bool Send(byte[] bytes, bool appendNewLine)
        {
            if (appendNewLine)
            {
                Array.Resize(ref bytes, bytes.Length + 2);
                bytes[bytes.Length - 2] = (byte)Keys.LineFeed;
                bytes[bytes.Length - 1] = (byte)Keys.Return;
            }
            return Send(bytes);
        }

        public Socket OpenPort()
        {
            var freePortProvider = new FreePortProvider();
            return OpenPort(freePortProvider.GetFreePort());
        }

        public Socket OpenPort(int port)
        {
            var socket = new Socket(Socket.AddressFamily, Socket.SocketType, Socket.ProtocolType);
            socket.Bind(new IPEndPoint(((IPEndPoint)Socket.LocalEndPoint).Address, port));
            socket.Listen(MaxPendingConnection);

            Task.Factory.StartNew(() =>
            {
                ListenerEngine(socket);
            });
            return socket;
        }

        private void ListenerEngine(Socket socket)
        {
            while (true)
            {
                if (socket.Poll(10, SelectMode.SelectRead))
                {
                    socket.BeginAccept(AcceptCallback, socket);
                }
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            var handler = socket.EndAccept(ar);
            handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ContinueAcceptCallback, handler);
        }

        private void ContinueAcceptCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            try
            {
                if (!socket.Connected)
                {
                    return;
                }
                var read = socket.EndReceive(ar);
                if (read <= 0)
                {
                    return;
                }

                var currentReadBuffer = new byte[read];
                Array.Copy(buffer, 0, currentReadBuffer, 0, read);
                OnDataArrived(new DataArrivedEventArgs(Tag, socket, (IPEndPoint)socket.RemoteEndPoint, currentReadBuffer));
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ContinueAcceptCallback, socket);
            }
            catch
            {
                var socketCloser = new SocketCloser();
                socketCloser.Close(socket);
            }
        }

        public override string ToString()
        {
            try
            {
                return $"Remote EP: {Socket.RemoteEndPoint}, Local EP: {Socket.LocalEndPoint}";
            }
            catch (Exception ex)
            {
                return $"Socket error occured: {ex.Message}";
            }
        }
    }
}
