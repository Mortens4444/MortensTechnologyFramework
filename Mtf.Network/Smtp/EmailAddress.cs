using System.Net.Mail;

namespace Mtf.Network.Smtp
{
    public class EmailAddress
    {
        public MailAddress MailAddress { get; }

        public EmailAddress(string emailAddress, string displayName = "")
        {
            MailAddress = new MailAddress(emailAddress, displayName);
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress.MailAddress.Address;
        }

        public static implicit operator EmailAddress(string emailAddress)
        {
            return new EmailAddress(emailAddress);
        }
    }
}