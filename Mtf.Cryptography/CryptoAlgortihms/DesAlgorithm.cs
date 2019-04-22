using System.Security.Cryptography;

namespace Mtf.Cryptography.CryptoAlgortihms
{
    public class DesAlgorithm : CryptingBase
    {
        public DesAlgorithm(byte[] key, byte[] initializationVector)
            : base(key, initializationVector,
                new KeyAndInitializationVectorLength(8, 8))
        {
            using (var cryptoServiceProvider = new DESCryptoServiceProvider())
            {
                encryptor = cryptoServiceProvider.CreateEncryptor(key, initializationVector);
                decryptor = cryptoServiceProvider.CreateDecryptor(key, initializationVector);
            }
        }
    }
}