using System.Linq;

namespace Mtf.Utils.CharExtensions
{
    public static class Password
    {
        public static bool IsBadPasswordChar(this char value)
        {
            return PasswordCharacters.BadPasswordChar.Any(ch => ch == value);
        }
    }
}