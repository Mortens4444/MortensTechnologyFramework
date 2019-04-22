using System;

namespace Mtf.Utils.StringExtensions
{
    public static class Grammar
    {
        public static bool IsContainsNumber(this string value)
        {
            foreach (var ch in value)
            {
                if (Char.IsDigit(ch))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsContainsLowerLetter(this string value)
        {
            for (var i = 0; i < value.Length; i++)
                if (Char.IsLetter(value[i]) && Char.IsLower(value[i])) return true;
            return false;
        }

        public static bool IsContainsUpperLetter(this string value)
        {
            for (var i = 0; i < value.Length; i++)
                if (Char.IsLetter(value[i]) && Char.IsUpper(value[i])) return true;
            return false;
        }

        public static bool IsContainsLetter(this string value)
        {
            for (var i = 0; i < value.Length; i++)
                if (Char.IsLetter(value[i])) return true;
            return false;
        }

        public static bool IsContainsSpecial(this string value)
        {
            for (var i = 0; i < value.Length; i++)
                if (!Char.IsLetterOrDigit(value[i])) return true;
            return false;
        }

        public static bool IsContainsSpecialLetterAndDigit(this string value)
        {
            var special = value.IsContainsSpecial();
            if (special)
            {
                for (var i = 0; i < value.Length; i++)
                    if (Char.IsLetterOrDigit(value[i])) return true;
            }
            return false;
        }
    }
}