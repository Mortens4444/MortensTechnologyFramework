using System.Security.Cryptography;

namespace Mtf.Cryptography.CryptoAlgortihms
{
    public class RijndaelAlgorithm : CryptingBase
    {
        public RijndaelAlgorithm(byte[] key, byte[] initializationVector)
        : base(key, initializationVector,
            new KeyAndInitializationVectorLength(32, 16))
        {
            using (var cryptoServiceProvider = Rijndael.Create())
            {
                encryptor = cryptoServiceProvider.CreateEncryptor(key, initializationVector);
                decryptor = cryptoServiceProvider.CreateDecryptor(key, initializationVector);
            }
        }
    }
}