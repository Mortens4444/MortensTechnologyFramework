/*using System.Security.Cryptography;

namespace Mtf.Cryptography.CryptoAlgortihms
{
    public class EccAlgorithm : CryptingBase
    {
        //private readonly byte[] initializationVector;

        public EccAlgorithm(CngKey ownPrivateCngKey, CngKey otherPartyPublicCngKey)
            : base(null, null)
        {
            var ellipticCurveDhCng = new ECDiffieHellmanCng(ownPrivateCngKey);
            ellipticCurveDhCng.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            ellipticCurveDhCng.HashAlgorithm = CngAlgorithm.Sha256;

            using (var cryptoServiceProvider = new AesCryptoServiceProvider())
            {
                cryptoServiceProvider.Key = ellipticCurveDhCng.DeriveKeyMaterial(otherPartyPublicCngKey);
                //initializationVector = cryptoServiceProvider.IV;
                encryptor = cryptoServiceProvider.CreateEncryptor();
                decryptor = cryptoServiceProvider.CreateDecryptor();
            }
        }
    }
}*/