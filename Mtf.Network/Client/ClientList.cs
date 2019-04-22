// TODO Fix class
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.InteropServices;
//using System.Text;
//using Mtf.Network.Sockets;
//using Mtf.Utils;
//
//namespace Mtf.Network.Client
//{
//    public class ClientList
//    {
//        public List<ClientObject> Clients { get; set; }
//        protected object sync;
//
//        public ClientList()
//        {
//            Clients = new List<ClientObject>();
//            sync = new object();
//        }
//
//        ~ClientList()
//        {
//            Dispose(false);
//        }
//
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//
//        protected virtual void Dispose(bool disposing)
//        {
//            lock (sync)
//            {
//                var sockerCloser = new SocketCloser();
//                foreach (var co in Clients)
//                {
//                    sockerCloser.Close(co.socket);
//                    ThreadUtils.StopThread(co.trd);
//                    if (co.socket.Connected) throw new Win32Exception(Marshal.GetLastWin32Error());
//                }
//
//                Clients.Clear();
//            }
//        }
//
//        public int NumberOfClients
//        {
//            get
//            {
//                lock (sync)
//                {
//                    return Clients.Count;
//                }
//            }
//        }
//
//        private Socket GetClientSocket(int clientListenerPort)
//        {
//            lock (sync)
//            {
//                return
//                    (from t in Clients where ((IPEndPoint) t.socket.RemoteEndPoint).Port == clientListenerPort select t.socket).
//                    FirstOrDefault();
//            }
//        }
//
//        void RemoveClientBySocket(Socket s)
//        {
//            var client = FindClient(s);
//            if (client == null)
//                return;
//
//            lock (sync)
//            {
//                if (Clients.Contains(client))
//                    Clients.Remove(client);
//            }
//        }
//
//        //readonly object send_lock = new object();
//        public void SendToAll_Client(string message)
//        {
//            lock (sync)
//            {
//                foreach (var client_object in Clients)
//                {
//                    //lock (send_lock)
//                    Send(client_object.socket, Encoding.GetBytes(message));
//                    //if (Send(client_object.socket, Encoding.GetBytes(message)))
//                    //Thread.Sleep(100);
//                    //	Console.WriteLine("Message sent.");
//                    //InfoBox.Show("SendToAll_Client", client_object + " - " + message);
//                }
//            }
//        }
//
//        public ClientObject FindClient(Socket clientSocket)
//        {
//            lock (sync)
//            {
//                return Clients != null ? Clients.FirstOrDefault(t => t.socket.Equals(clientSocket)) : null;
//            }
///*			List<ClientObject> client_list = new List<ClientObject>();
//			foreach (ClientObject co in this.clients) client_list.Add(co);
//			foreach (ClientObject co in client_list) if (co.socket.Equals(client_socket)) return co;*/
//        }
//
//        public ClientObject FindClient(EndPoint ipEndpoint)
//        {
//            lock (sync)
//            {
//                return Clients != null ? Clients.FirstOrDefault(t => t.socket.RemoteEndPoint.Equals(ipEndpoint)) : null;
//            }
//            /*List<ClientObject> client_list = new List<ClientObject>();
//            foreach (ClientObject co in this.clients) client_list.Add(co);
//            foreach (ClientObject co in client_list) if (co.socket.RemoteEndPoint.Equals((IPEndPoint)ip_endpoint)) return co;*/
//        }
//
//        public void Add(ClientObject co)
//        {
//            lock (sync)
//            {
//                Clients.Add(co);
//
//                // TODO: use ClientBase Disconnected event to remove clients
//            }
//        }
//
//        public bool Send(int clientListenerPort, string message)
//        {
//            var s = GetClientSocket(clientListenerPort);
//            return s != null && Send(s, Encoding.GetBytes(message));
//        }
//
//        public void ClosePort(int port)
//        {
//            if (Clients == null)
//                return;
//
//            lock (sync)
//            {
//                foreach (var client_object in Clients.Where(client_object => ((IPEndPoint) client_object.socket.LocalEndPoint).Port == port))
//                {
//                    client_object.socket.Shutdown(SocketShutdown.Both);
//                    try
//                    {
//                        client_object.socket.Disconnect(false);
//                    }
//                    catch
//                    {
//                    }
//                    client_object.socket.Close();
//                    ThreadUtils.StopThread(client_object.trd);
//                    //if (clients[i].trd != null) if (clients[i].trd.IsAlive) clients[i].trd.Abort();
//                    Clients.Remove(client_object);
//                    //sockets.Remove(s);
//                    break;
//                }
//            }
//
//            /*List<ClientObject> client_list = new List<ClientObject>();
//            foreach (ClientObject co in this.clients) client_list.Add(co);
//            foreach (ClientObject co in client_list)
//            {
//                if (((IPEndPoint)co.socket.LocalEndPoint).Port == port)
//                {
//                    co.socket.Shutdown(SocketShutdown.Both);
//                    try { co.socket.Disconnect(false); } catch { }
//                    co.socket.Close();
//                    if (co.trd != null) if (co.trd.IsAlive) co.trd.Abort();
//                    this.clients.Remove(co);
//                    //sockets.Remove(s);
//                    break;
//                }
//            }*/
//
//            /*foreach (Socket s in this.sockets)
//            {
//                if (((IPEndPoint)s.LocalEndPoint).Port == port)
//                {
//                    s.Shutdown(SocketShutdown.Both);
//                    try { s.Disconnect(false); } catch { }
//                    s.Close();
//                    sockets.Remove(s);
//                    break;
//                }
//            }*/
//        }
//
//        /*public bool Send(Socket socket, byte[] bytes)
//        {
//            if (bytes == null || bytes.Length == 0)
//            {
//                return false;
//            }
//
//            var sentBytes = 0;
//            var success = false;
//
//            try
//            {
//                if (socket.Connected)
//                {
//                    sentBytes = socket.Send(bytes, bytes.Length, SocketFlags.None);
//                }
//                else
//                {
//                    OnDisconnection(new DisconnectionEventArgs(this));
//                }
//                success = sentBytes == bytes.Length;
//            }
//            catch (SocketException)
//            {
//                OnDisconnection(new DisconnectionEventArgs(this));
//            }
//
//            return success;
//        }
//
//        public bool Send(Socket socket, string message)
//        {
//            return Send(socket, Encoding.GetBytes(message));
//        }
//    }
//}