namespace Mtf.Mailer
{
    public class SmtpServer
    {
        public string Host { get; }

        public int Port { get; }

        public bool Ssl { get; }

        public bool RequiresAuthentication { get; }

        public bool ForceAuthenticationMethod { get; }

        public SmtpAuthentication SmtpAuthentication { get; }

        public string Username { get; }

        public string Password { get; }

        public SmtpServer(string host, int port = 25, bool ssl = false,
            string username = null, string password = null, SmtpAuthentication? smtpAuthentication = null)
        {
            Host = host;
            Port = port;
            Ssl = ssl;

            RequiresAuthentication = username != null || password != null;

            Username = username;
            Password = password;

            ForceAuthenticationMethod = smtpAuthentication.HasValue;
            if (smtpAuthentication.HasValue)
            {
                SmtpAuthentication = smtpAuthentication.Value;
            }
        }
    }
}
