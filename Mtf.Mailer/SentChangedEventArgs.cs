using System;
using System.Collections.Generic;

namespace Mtf.Mailer
{
    public class SentChangedEventArgs : EventArgs
    {
        public List<object> Arguments { get; }

        public bool Sent { get; }

        public MailHeader[] Headers { get; }

        public Exception Exception { get; }

        public SentChangedEventArgs(bool sent, MailHeader[] headers, Exception exception, List<object> arguments = null)
        {
            Sent = sent;
            Headers = headers;
            Exception = exception;
            Arguments = arguments;
        }
    }
}