using System.Collections.Generic;
using System.Linq;

namespace Mtf.Cryptography.Crypting
{
    public class ByteReplaceCypher : IByteCypher
    {
        private readonly int replaceKey;

        public ByteReplaceCypher(int replaceKey)
        {
            this.replaceKey = replaceKey;
        }

        public IEnumerable<byte> Encrypt(IEnumerable<byte> source)
        {
            var result = source as byte[] ?? source.ToArray();
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(result[i] + replaceKey);
            }
            return result;
        }

        public IEnumerable<byte> Decrypt(IEnumerable<byte> cryptedText)
        {
            return Encrypt(cryptedText);
        }
    }
}