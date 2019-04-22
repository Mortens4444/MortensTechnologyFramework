using System;
using System.Collections;
using System.Text;

namespace Mtf.Utils.StringExtensions
{
    public static class Combinatorics
    {
        private const int NotFound = -1;
        
        public static string GetNext(this string value)
        {
            return BruteForce(ref value, 0, 255);
        }

        public static string GetNext(this string value, int firstCharcode, int lastCharcode)
        {
            return BruteForce(ref value, firstCharcode, lastCharcode);
        }

        public static string GetNext(this string value, char firstChar, char lastChar)
        {
            return BruteForce(ref value, firstChar, lastChar);
        }

        public static string GetNext(this string value, char[] chars)
        {
            return BruteForce(ref value, chars);
        }
        
        public static string BruteForce(ref string basicPassword, char firstChar, char lastChar)
        {
            return BruteForce(ref basicPassword, Convert.ToInt32(firstChar), Convert.ToInt32(lastChar));
        }

        public static string BruteForce(ref string basicPassword, char[] chars)
        {
            if (basicPassword == null)
            {
                return String.Empty;
            }
            var firstChar = chars[0];
            var lastChar = chars[chars.Length - 1];

            var password = GetNextPassword(basicPassword, chars, lastChar, firstChar, GetNextPassword1);
            return password.ToString().Reverse();
        }

        public static string BruteForce(ref string basicPassword, int firstCharcode, int lastCharcode)
        {
            if (basicPassword == null)
            {
                return String.Empty;
            }
            if (firstCharcode > lastCharcode)
            {
                IntExtensions.BaseExtensions.Swap(ref firstCharcode, ref lastCharcode);
            }
            var firstChar = (char)firstCharcode;
            var lastChar = (char)lastCharcode;

            var password = GetNextPassword(basicPassword, null, lastChar, firstChar, GetNextPassword2);
            return password.ToString().Reverse();
        }

        private static char GetNextPassword1(string basicPassword, char[] chars, int lastCharacterIndex)
        {
            var charIndex = Array.IndexOf(chars, basicPassword[lastCharacterIndex--]);
            return chars[++charIndex];
        }

        private static char GetNextPassword2(string basicPassword, char[] chars, int lastCharacterIndex)
        {
            int code = basicPassword[lastCharacterIndex--];
            return (char)++code;
        }

        private delegate char GetNextCharCallback(string basicPassword, char[] chars, int lastCharacterIndex);

        private static StringBuilder GetNextPassword(string basicPassword, char[] chars, char lastChar, char firstChar, GetNextCharCallback getNextCharCallback)
        {
            var password = new StringBuilder();
            var lastCharacterIndex = basicPassword.Length - 1;

            if (lastCharacterIndex > NotFound)
            {
                while (lastCharacterIndex > NotFound && basicPassword[lastCharacterIndex] == lastChar)
                {
                    password.Append(firstChar);
                    lastCharacterIndex--;
                }

                if (lastCharacterIndex > NotFound)
                {
                    var nextChar = getNextCharCallback(basicPassword, chars, lastCharacterIndex);
                    password.Append(nextChar);
                }
                else
                {
                    password.Append(firstChar);
                }

                for (var i = lastCharacterIndex; i >= 0; i--)
                {
                    password.Append(basicPassword[i]);
                }
            }
            else
            {
                password.Append(firstChar);
            }
            return password;
        }

        public static void GetPermutations(string letters, ref ArrayList result, string fixedLetters)
        {
            var i = 0;
            while (i < letters.Length)
            {
                var tmp = fixedLetters + letters;
                if (!result.Contains(tmp)) result.Add(tmp);

                if (letters.Substring(1).Length > 1)
                {
                    GetPermutations(letters.Substring(1), ref result, fixedLetters + letters[0]);
                }

                int k = 1, l = 0;
                var sb = new StringBuilder(letters);
                while (l < letters.Length)
                {
                    if (k >= letters.Length) k -= letters.Length;
                    sb[l] = letters[k];
                    k++;
                    l++;
                }
                letters = sb.ToString();

                i++;
            }
        }
    }
}