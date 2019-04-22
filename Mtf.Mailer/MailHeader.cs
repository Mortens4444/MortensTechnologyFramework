namespace Mtf.Mailer
{
    public class MailHeader
    {
        public string Name { get; }
        public string Value { get; }

        public MailHeader(string name, object value)
        {
            Name = name;
            Value = value.ToString();
        }
    }
}