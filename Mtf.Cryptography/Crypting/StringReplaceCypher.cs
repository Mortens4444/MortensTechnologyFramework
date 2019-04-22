using System.Text;

namespace Mtf.Cryptography.Crypting
{
    public class StringReplaceCypher
    {
        private readonly int replaceKey;

        public StringReplaceCypher(int replaceKey)
        {
            this.replaceKey = replaceKey;
        }

        public string ReplaceCypher(string source)
        {
            var result = new StringBuilder(source);
            for (var i = 0; i < result.Length; i++)
            {
                var newChar = (char)(source[i] + replaceKey);
                result[i] = newChar != 0 ? newChar : source[i];
            }
            return result.ToString();
        }
    }
}