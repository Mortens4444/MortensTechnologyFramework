using System;
using System.Linq;

namespace Mtf.Utils.CharExtensions
{
    public static class Cryptography
    {
        public static readonly string Chars = "aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyzAÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ0123456789§'\"+!%/=(),.-?:_;*<>#&@{}[]đĐ\\|€~ˇ^˘°˛`˙´˝¨¸÷×$ß¤łŁ";
        public static readonly char[] Base64CharArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };

        public static bool IsBadPasswordChar(this char value)
        {
            return PasswordCharacters.BadPasswordChar.Any(ch => ch == value);
        }

        public static char GetCodedChar(this char value)
        {
            switch (value)
            {
                case '0':
                    return '0';
                case '1':
                    return 'l';
                case '3':
                    return 'e';
                case '4':
                    return 'a';
                case '5':
                    return 's';
                case 'a':
                case 'A':
                    return '4';
                case 'o':
                case 'O':
                case 'D':
                    return '0';
                case 'E':
                case 'e':
                    return '3';
                case 'i':
                    return '!';
                case 'I':
                case 'l':
                    return '1';
                case 's':
                case 'S':
                    return '5';
                default:
                    return value;
            }
        }

        public static byte CharToBase64Code(this char value)
        {
            for (var i = 0; i < 64; i++)
            {
                if (Base64CharArray[i] == value)
                {
                    return (byte)i;
                }
            }

            return 0;
        }

        public static char Base64CodeToChar(byte code)
        {
            if (code >= 64)
            {
                throw new ArgumentException($"Parameter name: {nameof(code)}", nameof(code));
            }

            return Base64CharArray[code];
        }
    }
}