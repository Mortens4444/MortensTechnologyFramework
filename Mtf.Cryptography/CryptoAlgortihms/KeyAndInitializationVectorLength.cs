namespace Mtf.Cryptography.CryptoAlgortihms
{
    public class KeyAndInitializationVectorLength
    {
        public int KeyLength { get; }

        public int InitializationVectorLength { get; }

        public KeyAndInitializationVectorLength(int keyLength, int initializationVectorLength)
        {
            KeyLength = keyLength;
            InitializationVectorLength = initializationVectorLength;
        }
    }
}