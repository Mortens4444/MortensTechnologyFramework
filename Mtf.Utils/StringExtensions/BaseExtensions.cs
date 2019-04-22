using System;
using System.Security;

namespace Mtf.Utils.StringExtensions
{
    public static class BaseExtensions
    {
        private const int NotFound = -1;

        public static bool ContainsAnyOf(this string value, params string[] texts)
        {
            foreach (var text in texts)
            {
                if (value.Contains(text))
                {
                    return true;
                }
            }
            return false;
        }

        public static char FirstChar(this string value)
        {
            return value[0];
        }

        public static char LastChar(this string value)
        {
            return value[value.Length - 1];
        }

        public static bool IsStartsAndEndWith(this string value, char startCh, char endCh)
        {
            return value.IsStartsWith(startCh) && value.IsEndWith(endCh);
        }

        public static bool IsStartsWith(this string value, char ch)
        {
            if (String.IsNullOrEmpty(value)) return false;
            return value[0] == ch;
        }

        public static bool IsEndWith(this string value, char ch)
        {
            if (String.IsNullOrEmpty(value)) return false;
            return value[value.Length - 1] == ch;
        }

        public static bool IsEqualOneOfThis(this string value, params string[] values)
        {
            return Generics.Equality.IsEqualOneOfThis(value, values);
        }

        public static string Substring(this string value, string firstElement)
        {
            var sIndex = value.IndexOf(firstElement);
            if (sIndex != NotFound)
            {
                sIndex += firstElement.Length;
                var eIndex = value.IndexOf(",", sIndex + 1);
                return eIndex == NotFound ? value.Substring(sIndex) : value.Substring(sIndex, eIndex - sIndex);
            }
            return String.Empty;
        }

        public static string Substring(this string value, string firstElement, string secondElement)
        {
            return value.Substring(firstElement, secondElement, false, 0);
        }

        public static string Substring(this string value, string firstElement, string secondElement, bool caseInsensitive)
        {
            return value.Substring(firstElement, secondElement, caseInsensitive, 0);
        }

        public static string Substring(this string value, string firstElement, string secondElement, int startIndex)
        {
            return value.Substring(firstElement, secondElement, false, startIndex);
        }

        public static string Substring(this string value, string firstElement, string secondElement, bool caseInsensitive, int startIndex)
        {
            var sIndex = caseInsensitive ? value.IndexOf(firstElement, startIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(firstElement, startIndex);

            if (sIndex != NotFound)
            {
                sIndex += firstElement.Length;

                var eIndex = caseInsensitive ? value.IndexOf(secondElement, sIndex, StringComparison.CurrentCultureIgnoreCase) : value.IndexOf(secondElement, sIndex);

                return eIndex == NotFound ? value.Substring(sIndex) : value.Substring(sIndex, eIndex - sIndex);
            }
            return String.Empty;
        }

        public static string Reverse(this string value)
        {
            var charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new String(charArray);
        }

        public static string EscapeString(this string value)
        {
            return SecurityElement.Escape(value);
        }

        public static string Remove(this string value, string removable)
        {
            return value.Replace(removable, String.Empty);
        }

        /// <summary>
        /// Creates a byte array from a string if it's in the correct format. Ex: [12][243][124][68]
        /// </summary>
        /// <param name="value">String representation of the byte array. Ex: [12][243][124][68]</param>
        /// <returns>A byte array. Ex: new byte[] { 12, 243, 124, 68 };</returns>
        public static byte[] ArrayStringToArray(this string value)
        {
            var byteStrings = value.Split('[', ']');
            var bytes = new byte[byteStrings.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(byteStrings[2 * i + 1]);
            }
            return bytes;
        }

        public static string SplitedWithIndex(this string value, char separator, int index)
        {
            var resultArray = value.Split(new [] { separator }, StringSplitOptions.None);
            return resultArray[index];
        }

        public static string SplitedWithIndex(this string value, string separator, int index)
        {
            var resultArray = value.Split(new [] { separator }, StringSplitOptions.None);
            return resultArray[index];
        }

        public static string[] Split(this string value, string separator)
        {
            return value.Split(new [] { separator }, StringSplitOptions.None);
        }

        public static string[] Split(this string value, string separator, StringSplitOptions options)
        {
            return value.Split(new [] { separator }, options);
        }

        public static string[] SplitOnNewLines(this string value)
        {
            return value.SplitOnNewLines(StringSplitOptions.None);
        }

        public static string[] SplitOnNewLines(this string value, StringSplitOptions options)
        {
            return value.Split(new [] { "\r\n", "\r" }, options);
        }

        public static string[] SplitOnDoubleNewLines(this string value)
        {
            return value.Split(new [] { "\r\n\r\n" }, StringSplitOptions.None);
        }

        public static bool IsLessThan(this string a, string b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool IsLessOrEqualThan(this string a, string b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool IsGreaterThan(this string a, string b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool IsGreaterOrEqualThan(this string a, string b)
        {
            return a.CompareTo(b) >= 0;
        }

        public static string TruncateOnChars(this string value, params char[] chars)
        {
            var minLength = value.Length;
            foreach (var ch in chars)
            {
                var index = value.IndexOf(ch);
                if (index > NotFound && minLength > index)
                {
                    minLength = index;
                }
            }
            return value.Substring(0, minLength);
        }
    }
}