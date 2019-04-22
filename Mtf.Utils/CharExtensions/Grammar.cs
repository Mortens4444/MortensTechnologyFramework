using System.Linq;

namespace Mtf.Utils.CharExtensions
{
    public static class Grammar
    {
        /// <summary>
        /// Is this character is a vowel(magánhangzó)?
        /// </summary>
        /// <param name="value">Examined character</param>
        /// <returns>True if the character is a vowel, otherwise false.</returns>
        public static bool IsVowel(this char value)
        {
            return PasswordCharacters.Vowels.Any(ch => ch == value);
        }

        /// <summary>
        /// Is this character is a consonant(mássalhangzó)?
        /// </summary>
        /// <param name="value">Examined character</param>
        /// <returns>True if the character is a consonant, otherwise false.</returns>
        public static bool IsConsonant(this char value)
        {
            return PasswordCharacters.Consonant.Any(ch => ch == value);
        }
    }
}