using System;
using System.Net;
using System.Net.Sockets;

namespace Mtf.Network.Client
{
    public class DataArrivedEventArgs : EventArgs
    {
        public object Tag { get; }

        public IPEndPoint Sender { get; }

        public Socket Socket { get; }

        public byte[] Response { get; }

        public DataArrivedEventArgs(object tag, Socket socket, IPEndPoint sender, byte[] response)
        {
            Tag = tag;
            Socket = socket;
            Sender = sender;
            Response = response;
        }
    }
}