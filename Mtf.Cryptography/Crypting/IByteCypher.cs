using System.Collections.Generic;

namespace Mtf.Cryptography.Crypting
{
    public interface IByteCypher
    {
        IEnumerable<byte> Encrypt(IEnumerable<byte> plainText);

        IEnumerable<byte> Decrypt(IEnumerable<byte> cryptedText);
    }
}