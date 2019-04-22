using System;
using System.Collections.Generic;
using System.Text;
using Mtf.Utils.CharExtensions;

namespace Mtf.Utils.StringExtensions
{
    public static class NumberSystems
    {
        private static readonly Dictionary<int, int> DefaultNumberSystems = new Dictionary<int, int>
        {
            { 2, 8 },
            { 16, 2 }
        };

        public static string ConvertBinaryToText(this string value)
        {
            return ConvertNumberSystemToText(value, 2, DefaultNumberSystems[2]);
        }

        public static string ConvertHexaToText(this string value)
        {
            return ConvertNumberSystemToText(value, 16, DefaultNumberSystems[16]);
        }

        public static string ConvertNumberSystemToText(this string value, int fromBase, int length)
        {
            var sb = new StringBuilder();
            while (value.Length > 0)
            {
                string byteStr;
                if (value.Length > length - 1)
                {
                    byteStr = value.Substring(0, length);
                    value = value.Substring(length);
                }
                else
                {
                    byteStr = value;
                    value = String.Empty;
                }
                sb.Append(Convert.ToChar(Convert.ToByte(byteStr, fromBase)));
            }
            return sb.ToString();
        }

        public static string ConvertTextToBinary(this string value)
        {
            return ConvertTextToNumberSystem(value, 2, DefaultNumberSystems[2]);
        }

        public static string ConvertTextToHexa(this string value)
        {
            return ConvertTextToNumberSystem(value, 16, DefaultNumberSystems[16]);
        }

        public static string ConvertTextToNumberSystem(this string value, int toBase, int totalWidth)
        {
            var sb = new StringBuilder();
            foreach (var ch in value)
            {
                var converted = Convert.ToString(ch, toBase);
                var convertedWithPad = converted.PadLeft(totalWidth, '0');
                sb.Append(convertedWithPad);
            }
            return sb.ToString();
        }

        public static string HexaToDecimal(this string value)
        {
            return Convert.ToInt64(value, 16).ToString();
            //return long.Parse(value, System.Globalization.NumberStyles.HexNumber).ToString();
        }

        public static int HexaToInteger(this string value)
        {
            var result = 0;
            const string hex = "0123456789ABCDEF";
            for (var i = 0; i < value.Length; i++)
            {
                result = result * 16 + hex.IndexOf(Char.ToUpper(value[i]));
            }
            return result;
        }

        // TODO: Rename function according to coding style
        public static string HexaStringToASCII(this string value)
        {
            if (value.Length % 2 != 0)
            {
                throw new ArgumentException("String length must be even.", nameof(value));
            }

            var result = new StringBuilder();
            for (var i = 0; i < value.Length / 2; i++)
            {
                result.Append((char)Convert.ToInt32(value.Substring(i * 2, 2), 16));
            }
            return result.ToString();
        }

        public static string ASCIIToHexaString(this string value)
        {
            var result = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
            {
                result.Append(value[i].CharToHexaRepresentation());
            }
            return result.ToString();
        }

        public static byte BinaryToDecimalByte(this string value)
        {
            return Convert.ToByte(value, 2);
        }

        /*public static byte BinaryStringToByte(string bit_address)
        {
            if ((Regex.Replace(bit_address, "[^01]", "").Length != bit_address.Length) || (bit_address.Length > 8)) throw new ArgumentException(Constants.PARAMETER_ERROR, "bit_address");

            byte address = 0;
            for (int i = 0; i < bit_address.Length; i++)
            {
                if (bit_address[i] == '1')
                {
                    address += (byte)Math.Pow(2, 6 - i);
                }
            }

            return address;
        }*/
    }
}