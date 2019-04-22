using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Mtf.Network.Client;
using Mtf.Network.Sockets;
using Mtf.Utils.EnumExtensions;
using Mtf.Utils.StringExtensions;

namespace Mtf.Network.Ftp
{
    public class FtpClient : ClientBase
    {
        //private TcpListener dataSocket;

        public event DataArrivedEventHandler FileDataArrived;

        /// <summary>
        /// <see href="https://www.ietf.org/rfc/rfc959.txt">RFC-959</see>
        /// </summary>
        /// <param name="serverHostnameOrIpAddress"></param>
        /// <param name="dataArrived"></param>
        /// <param name="fileDataArrived"></param>
        public FtpClient(string serverHostnameOrIpAddress, DataArrivedEventHandler dataArrived,
            DataArrivedEventHandler fileDataArrived)
            : base(serverHostnameOrIpAddress, dataArrived, (ushort)ClientType.FTP_CONTROL)
        {
            FileDataArrived = fileDataArrived;
        }

        protected virtual void OnFileDataArrived(DataArrivedEventArgs e)
        {
            FileDataArrived?.Invoke(this, e);
        }

        /// <summary>
        /// This command tells the server to abort the previous FTP
        /// service command and any associated transfer of data. The
        /// abort command may require "special action", as discussed in
        /// the Section on FTP Commands, to force recognition by the
        /// server.  No action is to be taken if the previous command
        /// has been completed (including data transfer). The control
        /// connection is not to be closed by the server, but the data
        /// connection must be closed.
        /// </summary>
        public void Abort()
        {
            Send("ABOR\r\n");
        }

        public void ChangeToParentDirectory()
        {
            Send("CDUP\r\n");
        }

        /// <summary>
        /// This command does not affect any parameters or previously
        /// entered commands. It specifies no action other than that the
        /// server send an OK reply.
        /// </summary>
        public void NoOperation()
        {
            Send("NOOP\r\n");
        }

        /// <summary>
        /// This command shall cause the server to send helpful
        /// information regarding its implementation status over the
        /// control connection to the user. The command may take an
        /// argument (e.g., any command name) and return more specific
        /// information as a response. The reply is type 211 or 214.
        /// It is suggested that HELP be allowed before entering a USER
        /// command. The server may use this reply to specify
        /// site-dependent parameters, e.g., in response to HELP SITE.
        /// </summary>
        public void Help(string command = null)
        {
            Send(command == null ? "HELP\r\n" : $"HELP {command}\r\n");
        }

        /// <summary>
        /// This command requests the server-DTP to "listen" on a data
        /// port (which is not its default data port) and to wait for a
        /// connection rather than initiate one upon receipt of a
        /// transfer command.  The response to this command includes the
        /// host and port address this server is listening on.
        /// </summary>
        public void Passive()
        {
            Send("PASV\r\n");
        }

        public void PrintWorkingDirectory()
        {
            Send("PWD\r\n");
        }

        /// <summary>
        /// This command terminates a USER and if file transfer is not
        /// in progress, the server closes the control connection. If
        /// file transfer is in progress, the connection will remain
        /// open for result response and the server will then close it.
        /// If the user-process is transferring files for several USERs
        /// but does not wish to close and then reopen connections for
        /// each, then the REIN command should be used instead of QUIT.
        /// </summary>
        public void Logout()
        {
            Send("QUIT\r\n");
        }

        /// <summary>
        /// This command terminates a USER, flushing all I/O and account
        /// information, except to allow any transfer in progress to be
        /// completed.  All parameters are reset to the default settings
        /// and the control connection is left open.  This is identical
        /// to the state in which a user finds himself immediately after
        /// the control connection is opened.  A USER command may be
        /// expected to follow.
        /// </summary>
        public void Reinitialize()
        {
            Send("REIN\r\n");
        }

        /// <summary>
        /// This command behaves like STOR except that the resultant
        /// file is to be created in the current directory under a name
        /// unique to that directory. The 250 Transfer Started response
        /// must include the name generated.
        /// </summary>
        public void StoreUnique()
        {
            Send("STOU\r\n");
        }

        /// <summary>
        /// This command is used to find out the type of operating
        /// system at the server. The reply shall have as its first
        /// word one of the system names listed in the current version
        /// of the Assigned Numbers document [4].
        /// </summary>
        public void System()
        {
            Send("SYST\r\n");
        }

        public void User(string username)
        {
            Send($"USER {username}\r\n");
        }

        /// <summary>
        /// The argument field is a Telnet string specifying the user's
        /// password. This command must be immediately preceded by the
        /// user name command, and, for some sites, completes the user's
        /// identification for access control. Since password
        /// information is quite sensitive, it is desirable in general
        /// to "mask" it or suppress typeout. It appears that the
        /// server has no foolproof way to achieve this. It is
        /// therefore the responsibility of the user-FTP process to hide
        /// the sensitive password information.
        /// </summary>
        /// <param name="password"></param>
        public void Password(string password)
        {
            Send($"PASS {password}\r\n");
        }

        /// <summary>
        /// The argument field is a Telnet string identifying the user's
        /// account. The command is not necessarily related to the USER
        /// command, as some sites may require an account for login and
        /// others only for specific access, such as storing files. In
        /// the latter case the command may arrive at any time.
        /// </summary>
        /// <param name="account"></param>
        public void Account(string account)
        {
            Send($"ACCT {account}\r\n");
        }

        /// <summary>
        /// This command may be required by some servers to reserve
        /// sufficient storage to accommodate the new file to be
        /// transferred. The argument shall be a decimal integer
        /// representing the number of bytes (using the logical byte
        /// size) of storage to be reserved for the file. For files
        /// sent with record or page structure a maximum record or page
        /// size (in logical bytes) might also be necessary; this is
        /// indicated by a decimal integer in a second argument field of
        /// the command. This second argument is optional, but when
        /// present should be separated from the first by the three
        /// Telnet characters <SP> R <SP>. This command shall be
        /// followed by a STORe or APPEnd command. The ALLO command
        /// should be treated as a NOOP (no operation) by those servers
        /// which do not require that the maximum size of the file be
        /// declared beforehand, and those servers interested in only
        /// the maximum record or page size should accept a dummy value
        /// in the first argument and ignore it.
        /// </summary>
        /// <param name="numberOfBytes"></param>
        public void Allocate(decimal numberOfBytes)
        {
            Send($"ALLO {numberOfBytes}\r\n");
        }

        /// <summary>
        /// This command causes the server-DTP to accept the data
        /// transferred via the data connection and to store the data in
        /// a file at the server site. If the file specified in the
        /// pathname exists at the server site, then the data shall be
        /// appended to that file; otherwise the file specified in the
        /// pathname shall be created at the server site.
        /// </summary>
        /// <param name="param"></param>
        public void AppendOrCreate(string param)
        {
            Send($"APPE {param}\r\n");
        }

        /// <summary>
        /// This command allows the user to work with a different
        /// directory or dataset for file storage or retrieval without
        /// altering his login or accounting information.  Transfer
        /// parameters are similarly unchanged. The argument is a
        /// pathname specifying a directory or other system dependent
        /// file group designator.
        /// </summary>
        /// <param name="pathname"></param>
        public void ChangeWorkingDirectory(string pathname)
        {
            Send($"CWD {pathname}\r\n");
        }

        /// <summary>
        /// This command causes the file specified in the pathname to be
        /// deleted at the server site. If an extra level of protection
        /// is desired (such as the query, "Do you really wish to
        /// delete?"), it should be provided by the user-FTP process.
        /// </summary>
        /// <param name="pathname"></param>
        public void Delete(string pathname)
        {
            Send($"DELE {pathname}\r\n");
        }

        /// <summary>
        /// This command causes a list to be sent from the server to the
        /// passive DTP. If the pathname specifies a directory or other
        /// group of files, the server should transfer a list of files
        /// in the specified directory. If the pathname specifies a
        /// file then the server should send current information on the
        /// file. A null argument implies the user's current working or
        /// default directory. The data transfer is over the data
        /// connection in type ASCII or type EBCDIC. (The user must
        /// ensure that the TYPE is appropriately ASCII or EBCDIC).
        /// Since the information on a file may vary widely from system
        /// to system, this information may be hard to use automatically
        /// in a program, but may be quite useful to a human user.
        /// </summary>
        /// <param name="pathname"></param>
        public void List(string pathname = null)
        {
            Send(pathname == null ? "LIST\r\n" : $"LIST {pathname}\r\n");
        }

        /// <summary>
        /// Return the last-modified time of a specified file.
        /// </summary>
        /// <param name="filePath"></param>
        public void ModificationTime(string filePath)
        {
            Send($"MDTM {filePath}\r\n");
        }

        /// <summary>
        /// This command causes the directory specified in the pathname
        /// to be created as a directory (if the pathname is absolute)
        /// or as a subdirectory of the current working directory (if
        /// the pathname is relative).
        /// </summary>
        /// <param name="pathname"></param>
        public void MakeDirectory(string pathname)
        {
            Send($"MKD {pathname}\r\n");
        }

        /// <summary>
        /// The argument is a single Telnet character code specifying
        /// the data transfer modes described in the Section on
        /// Transmission Modes.
        /// The following codes are assigned for transfer modes:
        /// S - Stream
        /// B - Block
        /// C - Compressed
        ///
        /// The default transfer mode is Stream.
        /// </summary>
        /// <param name="transmissionMode"></param>
        public void TransferMode(TransmissionMode transmissionMode)
        {
            Send($"MODE {transmissionMode}\r\n");
        }

        /// <summary>
        /// This command causes a directory listing to be sent from
        /// server to user site. The pathname should specify a
        /// directory or other system-specific file group descriptor; a
        /// null argument implies the current directory. The server
        /// will return a stream of names of files and no other
        /// information. The data will be transferred in ASCII or
        /// EBCDIC type over the data connection as valid pathname
        /// strings separated by <CRLF> or <NL>. (Again the user must
        /// ensure that the TYPE is correct.) This command is intended
        /// to return information that can be used by a program to
        /// further process the files automatically. For example, in
        /// the implementation of a "multiple get" function.
        /// </summary>
        /// <param name="pathname"></param>
        public void NameList(string pathname)
        {
            Send($"NLST {pathname}\r\n");
        }

        /// <summary>
        /// The argument is a HOST-PORT specification for the data port
        /// to be used in data connection. There are defaults for both
        /// the user and server data ports, and under normal
        /// circumstances this command and its reply are not needed. If
        /// this command is used, the argument is the concatenation of a
        /// 32-bit internet host address and a 16-bit TCP port address.
        /// This address information is broken into 8-bit fields and the
        /// value of each field is transmitted as a decimal number (in
        /// character string representation). The fields are separated
        /// by commas. A port command would be:
        /// PORT h1,h2,h3,h4,p1,p2
        /// where h1 is the high order 8 bits of the internet host address.
        /// </summary>
        /// <param name="pathname"></param>
        public void DataPort(string pathname)
        {
            Send($"PORT {pathname}\r\n");
        }

        /// <summary>
        /// The argument field represents the server marker at which
        /// file transfer is to be restarted. This command does not
        /// cause file transfer but skips over the file to the specified
        /// data checkpoint. This command shall be immediately followed
        /// by the appropriate FTP service command which shall cause
        /// file transfer to resume.
        /// </summary>
        /// <param name="serviceCommand"></param>
        public void Restart(string serviceCommand)
        {
            Send($"REST {serviceCommand}\r\n");
        }

        /// <summary>
        /// This command causes the server-DTP to transfer a copy of the
        /// file, specified in the pathname, to the server- or user-DTP
        /// at the other end of the data connection. The status and
        /// contents of the file at the server site shall be unaffected.
        /// </summary>
        /// <param name="pathname"></param>
        public void Retrieve(string pathname)
        {
            Send($"RETR {pathname}\r\n");
        }

        /// <summary>
        /// This command causes the directory specified in the pathname
        /// to be removed as a directory (if the pathname is absolute)
        /// or as a subdirectory of the current working directory (if
        /// the pathname is relative). See Appendix II.
        /// </summary>
        /// <param name="pathname"></param>
        public void RemoteDirectory(string pathname)
        {
            Send($"RMD {pathname}\r\n");
        }

        /// <summary>
        /// This command specifies the old pathname of the file which is
        /// to be renamed. This command must be immediately followed by
        /// a "rename to" command specifying the new file pathname.
        /// </summary>
        /// <param name="pathname"></param>
        public void RenameFrom(string pathname)
        {
            Send($"RNFR {pathname}\r\n");
        }

        /// <summary>
        /// This command specifies the new pathname of the file
        /// specified in the immediately preceding "rename from"
        /// command. Together the two commands cause a file to be
        /// renamed.
        /// </summary>
        /// <param name="pathname"></param>
        public void RenameTo(string pathname)
        {
            Send($"RNTO {pathname}\r\n");
        }

        /// <summary>
        /// This command is used by the server to provide services
        /// specific to his system that are essential to file transfer
        /// but not sufficiently universal to be included as commands in
        /// the protocol. The nature of these services and the
        /// specification of their syntax can be stated in a reply to
        /// the HELP SITE command.
        /// </summary>
        /// <param name="pathname"></param>
        public void SiteParameters(string parameters)
        {
            Send($"SITE {parameters}\r\n");
        }

        /// <summary>
        /// Return the size of a file.
        /// </summary>
        /// <param name="pathname"></param>
        public void Size(string pathname)
        {
            Send($"SIZE {pathname}\r\n");
        }

        /// <summary>
        /// This command shall cause a status response to be sent over
        /// the control connection in the form of a reply. The command
        /// may be sent during a file transfer (along with the Telnet IP
        /// and Synch signals--see the Section on FTP Commands) in which
        /// case the server will respond with the status of the
        /// operation in progress, or it may be sent between file
        /// transfers. In the latter case, the command may have an
        /// argument field. If the argument is a pathname, the command
        /// is analogous to the "list" command except that data shall be
        /// transferred over the control connection. If a partial
        /// pathname is given, the server may respond with a list of
        /// file names or attributes associated with that specification.
        /// If no argument is given, the server should return general
        /// status information about the server FTP process. This
        /// should include current values of all transfer parameters and
        /// the status of connections.
        /// </summary>
        /// <param name="pathname"></param>
        public void Status(string pathname)
        {
            Send($"STAT {pathname}\r\n");
        }

        /// <summary>
        /// This command causes the server-DTP to accept the data
        /// transferred via the data connection and to store the data as
        /// a file at the server site. If the file specified in the
        /// pathname exists at the server site, then its contents shall
        /// be replaced by the data being transferred. A new file is
        /// created at the server site if the file specified in the
        /// pathname does not already exist.
        /// </summary>
        /// <param name="pathname"></param>
        public void Store(string pathname)
        {
            Send($"STOR {pathname}\r\n");
        }

        /// <summary>
        /// This command behaves like STOR except that the resultant
        /// file is to be created in the current directory under a name
        /// unique to that directory. The 250 Transfer Started response
        /// must include the name generated.
        /// </summary>
        /// <param name="pathname"></param>
        public void StoreUnique(string pathname)
        {
            Send($"STOU {pathname}\r\n");
        }

        /// <summary>
        /// The argument is a single Telnet character code specifying
        /// file structure described in the Section on Data
        /// Representation and Storage.
        ///
        /// The following codes are assigned for structure:
        ///
        /// F - File (no record structure)
        /// R - Record structure
        /// P - Page structure
        ///
        /// The default structure is File.
        /// </summary>
        /// <param name="structure"></param>
        public void Structure(Structure structure)
        {
            Send($"STRU {structure.GetDescription()}\r\n");
        }

        /// <summary>
        /// The argument specifies the representation type as described
        /// in the Section on Data Representation and Storage. Several
        /// types take a second parameter. The first parameter is
        /// denoted by a single Telnet character, as is the second
        /// Format parameter for ASCII and EBCDIC; the second parameter
        /// for local byte is a decimal integer to indicate Bytesize.
        /// The parameters are separated by a <SP> (Space, ASCII code
        /// 32).
        ///
        /// The following codes are assigned for type:
        ///
        ///           \    /
        /// A - ASCII |    | N - Non-print
        ///           |-><-| T - Telnet format effectors
        /// E - EBCDIC|    | C - Carriage Control (ASA)
        ///           /    \
        /// I - Image
        ///
        /// L <byte size> - Local byte Byte size
        ///
        /// The default representation type is ASCII Non-print. If the
        /// Format parameter is changed, and later just the first
        /// argument is changed, Format then returns to the Non-print
        /// default.
        /// </summary>
        /// <param name="representationType"></param>
        /// <param name="textInterpretation"></param>
        public void RepresentationType(RepresentationType representationType, TextInterpretation textInterpretation)
        {
            Send($"TYPE {representationType.GetDescription()} {textInterpretation.GetDescription()}\r\n");
        }

        public void RepresentationTypeToLocalFormat(RepresentationType representationType, decimal numberOfBitsPerByte = 8)
        {
            Send($"TYPE L {numberOfBitsPerByte}\r\n");
        }

        // TODO: Fix function
        /*private void Receiver()
        {
            while (Socket.Connected)
            {
                if (Socket.Available > 0)
                {
                    Thread.Sleep(200);
                    var readable = Socket.Available;

                    var receiveBuffer = new byte[readable];
                    var readBytes = Socket.Receive(receiveBuffer, receiveBuffer.Length, SocketFlags.None);

                    var stop = Environment.TickCount + 100000;
                    while (readBytes != readable && stop < Environment.TickCount)
                    {
                        readBytes += Socket.Receive(receiveBuffer, readBytes, receiveBuffer.Length - readBytes, SocketFlags.None);
                    }

                    if (readBytes > 0)
                    {
                        var receivedAnswer = new string(Encoding.GetChars(receiveBuffer, 0, readBytes));

                        // If it is an FTP connection, and data connection must be opened, then we open it here
                        if (receivedAnswer.ContainsAnyOf("150 ", "125 "))
                        {
                            while (!dataSocket.Pending())
                            {
                                Thread.Sleep(1);
                            }

                            //var timeout = DateTime.Now.AddSeconds(10);
                            var socket = dataSocket.AcceptSocket();

                            while (socket.Available == 0)
                            {
                                Thread.Sleep(1);
                            }

                            while (true)
                            {
                                var r = 0;
                                while (socket.Available <= 0 && r < 100000)
                                {
                                    Thread.Sleep(1);
                                    r++;
                                }
                                if (r == 100000 && socket.Available == 0)
                                {
                                    break;
                                }

                                var dataReceiveBuffer = new byte[socket.Available];
                                var bytes = socket.Receive(dataReceiveBuffer, dataReceiveBuffer.Length, SocketFlags.None);

                                var sb = new StringBuilder();
                                for (var i = 0; i < bytes; i++)
                                {
                                    sb.Append((char)dataReceiveBuffer[i]);
                                }

                                OnFileDataArrived(new DataArrivedEventArgs(Tag, socket, (IPEndPoint)Socket.RemoteEndPoint, dataReceiveBuffer));
                            }
                            var socketCloser = new SocketCloser();
                            socketCloser.Close(socket);
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }*/
    }
}