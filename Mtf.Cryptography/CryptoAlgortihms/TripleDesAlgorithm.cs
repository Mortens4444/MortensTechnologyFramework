using System.Security.Cryptography;

namespace Mtf.Cryptography.CryptoAlgortihms
{
    public class TripleDesAlgorithm : CryptingBase
    {
        public TripleDesAlgorithm(byte[] key, byte[] initializationVector)
            : base(key, initializationVector,
                new KeyAndInitializationVectorLength(16, 8),
                new KeyAndInitializationVectorLength(24, 8),
                new KeyAndInitializationVectorLength(24, 16))
        {
            using (var cryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                encryptor = cryptoServiceProvider.CreateEncryptor(key, initializationVector);
                decryptor = cryptoServiceProvider.CreateDecryptor(key, initializationVector);
            }
        }
    }
}