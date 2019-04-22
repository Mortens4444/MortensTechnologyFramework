using System.Text;

namespace Mtf.Cryptography.Crypting
{
    public class RotateCypher : IStringCypher
    {
        private readonly int rotationKey;

        public RotateCypher(int rotationKey)
        {
            this.rotationKey = rotationKey;
        }

        public string Encrypt(string plainText)
        {
            return Crypt(plainText, rotationKey);
        }

        public string Decrypt(string cryptedText)
        {
            return Crypt(cryptedText, -rotationKey);
        }

        private static string Crypt(string source, int currentKey)
        {
            int i = 0, j = 0;
            var result = new StringBuilder(source);

            while (currentKey > source.Length)
            {
                currentKey -= source.Length;
            }
            while (currentKey < -source.Length)
            {
                currentKey += source.Length;
            }

            if (currentKey > 0) // rotate right
            {
                j = currentKey;
                while (i < source.Length)
                {
                    while (j >= source.Length)
                    {
                        j -= source.Length;
                    }
                    result[j] = source[i];
                    i++;
                    j++;
                }
            }
            else if (currentKey < 0) // rotate left
            {
                i += -currentKey;
                while (j < source.Length)
                {
                    while (i >= source.Length)
                    {
                        i -= source.Length;
                    }
                    result[j] = source[i];
                    i++;
                    j++;
                }
            }

            return result.ToString();
        }
    }
}