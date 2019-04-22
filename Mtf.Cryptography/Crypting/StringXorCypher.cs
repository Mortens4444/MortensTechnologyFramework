using System.Text;

namespace Mtf.Cryptography.Crypting
{
    public class StringXorCypher : IStringCypher
    {
        private readonly string password;

        public StringXorCypher(string password)
        {
            this.password = password;
        }

        public string Encrypt(string plainText)
        {
            int i = 0, j = 0;
            var result = new StringBuilder();

            while (i < plainText.Length)
            {
                var cryptedChar = plainText[i] != password[j] ? (char) (plainText[i] ^ password[j]) : plainText[i];
                result.Append(cryptedChar);

                i++;
                j++;
                if (j >= password.Length)
                {
                    j = 0;
                }
            }

            return result.ToString();
        }

        public string Decrypt(string cryptedText)
        {
            return Encrypt(cryptedText);
        }
    }
}