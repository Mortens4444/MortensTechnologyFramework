namespace Mtf.Mailer
{
    public enum SmtpAuthentication : byte
    {
        Negotiate,
        Ntlm,
        Digest,
        Login
    }
}