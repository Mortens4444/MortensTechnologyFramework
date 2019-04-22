namespace Mtf.Network.Icmp
{
    public class PingReplyMessage
    {
        public bool Success { get;  }

        public string Title { get;  }

        public string Message { get;  }

        public PingReplyMessage(bool success, string title, string message)
        {
            Title = title;
            Message = message;
            Success = success;
        }
    }
}