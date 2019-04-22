namespace Mtf.Network.Port
{
    public class Port
    {
        public byte P1 { get; set; }

        public byte P2 { get; set; }

        public Port(int port)
        {
            P1 = (byte)(port / 256);
            P2 = (byte)(port % 256);
        }

        public override string ToString()
        {
            return $"{P1},{P2}";
        }
    }
}