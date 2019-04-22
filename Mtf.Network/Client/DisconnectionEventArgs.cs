namespace Mtf.Network.Client
{
    public class DisconnectionEventArgs
    {
        public ClientBase Sender { get; set; }

        public DisconnectionEventArgs(ClientBase sender)
        {
            Sender = sender;
        }
    }
}