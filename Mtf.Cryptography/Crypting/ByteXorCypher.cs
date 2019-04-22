using System.Collections.Generic;
using System.Linq;

namespace Mtf.Cryptography.Crypting
{
    public class ByteXorCypher : IByteCypher
    {
        private readonly IEnumerable<byte> password;
        private readonly int passwordLength;

        public ByteXorCypher(IEnumerable<byte> password)
        {
            this.password = password;
            passwordLength = password.Count();
        }

        public IEnumerable<byte> Encrypt(IEnumerable<byte> plainText)
        {
            var count = plainText.Count();
            var result = new byte[count];
            int i = 0, j = 0;

            while (i < count)
            {
                result[i] = (byte)(plainText.ElementAt(i) ^ password.ElementAt(j));
                i++;
                j++;
                if (j >= passwordLength)
                {
                    j = 0;
                }
            }

            return result;
        }

        public IEnumerable<byte> Decrypt(IEnumerable<byte> cryptedText)
        {
            return Encrypt(cryptedText);
        }
    }
}