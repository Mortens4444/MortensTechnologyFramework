using System;
using System.Text;
using Mtf.Utils.StringExtensions;

namespace Mtf.Utils.ByteArrayExtensions
{
    public static class String
    {
        public static string ToArrayString(this byte[] array, int startIndex, int endIndex)
        {
            var result = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {
                result.AppendFormat($"[{array[i]}]");
            }
            return result.ToString();
        }

        /// <summary>
        /// Creates a byte array from a string if it's in the correct format. Ex: [12][243][124][68]
        /// </summary>
        /// <param name="toString">String representation of the byte array. Ex: [12][243][124][68]</param>
        /// <returns>A byte array. Ex: new byte[] { 12, 243, 124, 68 };</returns>
        public static byte[] CreateArray(string toString)
        {
            return toString.ArrayStringToArray();
        }

        public static string ToAsciiString(this byte[] value)
        {
            return Encoding.ASCII.GetString(value);
        }

        public static string AsciiGetString(this byte[] value)
        {
            return Encoding.ASCII.GetString(value);
        }

        public static string Utf8GetString(this byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }

        /// <summary>
        /// From a byte array creates a string
        /// </summary>
        /// <param name="value">The byte array</param>
        /// <returns>Ex.: [12][23][64][78]</returns>
        public static string ToArrayString(this byte[] value)
        {
            var result = new StringBuilder();
            foreach (var byteValue in value)
            {
                result.AppendFormat($"[{byteValue}]");
            }
            return result.ToString();
        }

        public static string ToAsciiStringZeroByteTerminated(this byte[] value)
        {
            var str = new StringBuilder();
            for (var i = 0; i < value.Length && value[i] != 0; i++)
            {
                str.Append(Convert.ToChar(value[i]));
            }

            return str.ToString();
        }
    }
}