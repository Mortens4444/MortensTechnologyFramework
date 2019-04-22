using Mtf.Network.Client;

namespace Mtf.Network.Smtp
{
    public class SmtpClient : ClientBase
    {
        /// <summary>
        /// <see href="https://tools.ietf.org/html/rfc821">RFC-821</see>
        /// </summary>
        /// <param name="serverHostOrIp"></param>
        /// <param name="dataArrivedHandler"></param>
        public SmtpClient(string serverHostOrIp, DataArrivedEventHandler dataArrivedHandler)
            : base(serverHostOrIp, dataArrivedHandler, (ushort)ClientType.SMTP)
        { }

        /// <summary>
        /// S: VRFY Smith
        /// R: 250 Fred Smith <Smith@USC-ISIF.ARPA>
        ///
        /// R: 251 User not local; will forward to <Smith@USC-ISIQ.ARPA>
        ///
        /// R: 550 String does not match anything.
        ///
        /// R: 551 User not local; please try <Jones@USC-ISIQ.ARPA>
        ///
        /// R: 553 User ambiguous.
        /// </summary>
        /// <param name="userName"></param>
        public void VerifyUser(string userName)
        {
            Send($"VRFY {userName}\r\n");
        }

        /// <summary>
        /// all | everyone | staff
        /// S: EXPN Example-People
        /// R: 250-Jon Postel <Postel@USC-ISIF.ARPA>
        /// R: 250-Fred Fonebone <Fonebone@USC-ISIQ.ARPA>
        /// R: 250-Sam Q. Smith <SQSmith@USC-ISIQ.ARPA>
        ///
        /// R: 550 Access Denied to You.
        /// </summary>
        /// <param name="listName"></param>
        public void ExpandMailingList(string listName)
        {
            Send($"EXPN {listName}\r\n");
        }

        public void SendFrom(string from)
        {
            Send($"SEND FROM:<{from}>\r\n");
        }

        public void SendOrMailFrom(string from)
        {
            Send($"SOML FROM:<{from}>\r\n");
        }

        public void SendAndMailFrom(string from)
        {
            Send($"SAML FROM:<{from}>\r\n");
        }

        /// <summary>
        /// This command is used to initiate a mail transaction in which
        /// the mail data is delivered to one or more mailboxes.  The
        /// argument field contains a reverse-path.
        /// </summary>
        /// <param name="from"></param>
        public void MailFrom(string from)
        {
            Send($"MAIL FROM:<{from}>\r\n");
        }

        /// <summary>
        /// This command is used to identify an individual recipient of
        /// the mail data; multiple recipients are specified by multiple
        /// use of this command.
        /// </summary>
        /// <param name="to"></param>
        public void RecipientTo(string to)
        {
            Send($"RCPT TO:<{to}>\r\n");
        }

        /// <summary>
        /// The receiver treats the lines following the command as mail
        /// data from the sender.  This command causes the mail data
        /// from this command to be appended to the mail data buffer.
        /// The mail data may contain any of the 128 ASCII character codes.
        /// </summary>
        public void Data()
        {
            Send("DATA\r\n");
        }

        public void SendMail(string from, string to, string message)
        {
            MailFrom(from);
            RecipientTo(to);
            Data();
            Send(message);
            Send("\r\n.");
        }

        /// <summary>
        /// This command specifies that the receiver must send an OK
        /// reply, and then close the transmission channel.
        /// </summary>
        public void Quit()
        {
            Send("QUIT\r\n");
        }

        public void Reset()
        {
            Send("RSET\r\n");
        }

        /// <summary>
        /// This command does not affect any parameters or previously
        /// entered commands.  It specifies no action other than that
        /// the receiver send an OK reply.
        /// </summary>
        public void NoOperation()
        {
            Send("NOOP\r\n");
        }

        /// <summary>
        /// This command causes the receiver to send helpful information
        /// to the sender of the HELP command.  The command may take an
        /// argument (e.g., any command name) and return more specific
        /// information as a response.
        /// </summary>
        public void Help()
        {
            Send("HELP\r\n");
        }

        /// <summary>
        /// This command specifies that the receiver must either (1)
        /// send an OK reply and then take on the role of the
        /// sender-SMTP, or (2) send a refusal reply and retain the role
        /// of the receiver-SMTP.
        /// </summary>
        public void Turn()
        {
            Send("TURN\r\n");
        }

        /// <summary>
        /// This command is used to identify the sender-SMTP to the
        /// receiver-SMTP.  The argument field contains the host name of
        /// the sender-SMTP.
        /// </summary>
        public void Helo()
        {
            Send($"HELO {ServerHostnameOrIpAddress}\r\n");
        }

        public void Ehlo()
        {
            Send($"EHLO {ServerHostnameOrIpAddress}\r\n");
        }
    }
}