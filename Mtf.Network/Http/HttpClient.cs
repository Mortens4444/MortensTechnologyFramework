using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Mtf.Network.Client;
using Mtf.Network.Packets.Http;

namespace Mtf.Network.Http
{
    public class HttpClient : ClientBase
    {
        public HttpPacket WebRequest { get; set; }

        public HttpClient(string serverHostnameOrIpAddress, DataArrivedEventHandler dataArrived)
            : base(serverHostnameOrIpAddress, dataArrived, (ushort)ClientType.HTTP)
        { }

        // TODO: Fix function
        /*public void Send(string url, HttpMethod method)
        {
            WebRequest = new HttpPacket(method, url);
            Send(WebRequest.HTTP_packet);
        }

        // TODO: Fix function
        private void Receiver()
        {
            while (Socket.Connected)
            {
                Socket.Poll(-1, SelectMode.SelectRead);
                var readable = Socket.Available;
                var receiveBuffer = new byte[readable];
                var readBytes = Socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);

                var stop = Environment.TickCount + 100000;
                while (readBytes != readable && stop < Environment.TickCount)
                {
                    readBytes += Socket.Receive(receiveBuffer, readBytes, receiveBuffer.Length - readBytes, SocketFlags.None);
                }

                var s = new string(Encoding.GetChars(receiveBuffer, 0, readBytes));

                // HTTP See Other
                if (s.Substring(9, 3) == "303")
                {
                    var responseHeaders = s.Split(new[] { '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var responseHeader in responseHeaders)
                    {
                        if (responseHeader.IndexOf("Location:") != 0)
                        {
                            continue;
                        }

                        if (WebRequest != null)
                        {
                            // Automatic rewuest forwarding
                            WebRequest.URL = responseHeader.Substring(9).Trim();
                            Send(WebRequest.HTTP_packet);
                        }
                        break;
                    }
                }
                else
                {
                    OnDataArrived(new DataArrivedEventArgs(Tag, Socket, (IPEndPoint)Socket.RemoteEndPoint, receiveBuffer));
                }
                Thread.Sleep(1);
            }
        }*/
    }
}