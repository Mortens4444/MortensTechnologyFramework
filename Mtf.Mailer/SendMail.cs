using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Mtf.Core.Cryptography;
using Mtf.Messages.ErrorBox;
using Mtf.Messages.InfoBox;
using Mtf.Reflection.ExceptionInfo;

namespace Mtf.Mailer
{
    public class SendMail
    {
        public delegate void SentChangedEventHandler(object sender, SentChangedEventArgs e);

        public List<object> Arguments { get; set; }
        public bool ForceSmtpAuthentication { get; set; }
        public SmtpAuthentication SmtpAuthentication { get; set; }

        public event SentChangedEventHandler SentChanged;

        private readonly SmtpClient smtpClient;
        private MailHeader[] headers;
        private MailMessage mail;
        private readonly IBase64 base64;

        public SendMail(IBase64 base64, string smtpHost, bool sslEncryption, int port, string username = null, string password = null)
            : this(base64, new SmtpServer(smtpHost, port, sslEncryption, username, password, SmtpAuthentication.Digest))
        { }

        /// <summary>
        /// SendMail
        /// </summary>
        /// <param name="base64">The SMTP server</param>
        /// <param name="smtpServer">The SMTP server</param>
        public SendMail(IBase64 base64, SmtpServer smtpServer)
        {
            this.base64 = base64;
            CheckParameter(smtpServer.Host, String.Concat(nameof(smtpServer), ".", nameof(smtpServer.Host)));
            smtpClient = new SmtpClient(smtpServer.Host, smtpServer.Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = smtpServer.Ssl
            };
            smtpClient.SendCompleted += SendCompletedCallback;

            smtpClient.UseDefaultCredentials = !smtpServer.RequiresAuthentication;
            smtpClient.Credentials = smtpServer.RequiresAuthentication
                ? new NetworkCredential(smtpServer.Username, smtpServer.Password)
                : CredentialCache.DefaultNetworkCredentials;

            ForceSmtpAuthentication = true;
            SmtpAuthentication = SmtpAuthentication.Digest;
            Arguments = null;
        }

        public void Send(string sender, string recipient, string subject, string body)
        {
            Send(sender, recipient, null, null, null, subject, body);
        }

        public void Send(string sender, string recipient, MailHeader[] myHeaders, string subject, string body)
        {
            Send(sender, recipient, null, null, myHeaders, subject, body);
        }

        public void Send(string sender, string recipient, string carbonCopy, string blindCarbonCopy, MailHeader[] myHeaders, string subject, string body)
        {
            CheckParameter(sender, nameof(sender));
            CheckParameter(recipient, nameof(recipient));
            headers = myHeaders;
            mail = new MailMessage(sender, recipient);
            if (!string.IsNullOrEmpty(carbonCopy))
            {
                mail.CC.Add(carbonCopy);
            }
            if (!string.IsNullOrEmpty(blindCarbonCopy))
            {
                mail.Bcc.Add(blindCarbonCopy);
            }
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = mail.SubjectEncoding;
            if (myHeaders != null)
            {
                foreach (var myHeader in myHeaders)
                {
                    try
                    {
                        if (myHeader.Name != String.Empty && myHeader.Value != String.Empty)
                        {
                            mail.Headers.Add(base64.Encode(myHeader.Name), base64.Encode(myHeader.Value));
                        }
                    }
                    catch { }
                }
            }

            mail.Subject = subject;
            mail.Body = body;

            try
            {
                // NTLM (NT LAN Manager) Authentication /SMTP Extension/ throws System.FormatException - Invalid length for a Base-64 char array.
                if (ForceSmtpAuthentication)
                {
                    ForceSmtpClientAuthentication();
                }

                smtpClient.SendAsync(mail, null); //this.smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                OnSentChanged(new SentChangedEventArgs(false, headers, ex, Arguments));
            }
        }

        private void ForceSmtpClientAuthentication()
        {
            var transport = smtpClient.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
            if (transport != null)
            {
                var authenticationModules = transport.GetValue(smtpClient)
                    .GetType()
                    .GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);
                if (authenticationModules != null)
                {
                    var modulesArray = authenticationModules.GetValue(transport.GetValue(smtpClient)) as Array;
                    if (modulesArray != null)
                    {
                        var smtpAuthenticationModule = modulesArray.GetValue((byte) SmtpAuthentication);
                        modulesArray.SetValue(smtpAuthenticationModule, 0);
                        modulesArray.SetValue(smtpAuthenticationModule, 1);
                        modulesArray.SetValue(smtpAuthenticationModule, 2);
                        modulesArray.SetValue(smtpAuthenticationModule, 3);
                    }
                }
            }
        }

        private static void CheckParameter(string parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (parameter == String.Empty)
            {
                throw new ArgumentException("Parameter is empty srting", parameterName);
            }
        }

        public static void EmailSentChanged(SentChangedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            if (!e.Sent)
            {
                ErrorBox.Show("E-mail sent error", new ExceptionDetails(e.Exception).Details);
            }
            else
            {
                InfoBox.Show("E-mail succesfully sent", "E-mail sent was succesfully to target e-mail address");
            }
        }

        protected virtual void OnSentChanged(SentChangedEventArgs e)
        {
            SentChanged?.Invoke(this, e);
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            OnSentChanged(new SentChangedEventArgs(e.Error == null, headers, e.Error));
            mail.Dispose();
        }
    }
}