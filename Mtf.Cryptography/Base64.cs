using System;
using System.Text;
using Mtf.Core.Cryptography;

namespace Mtf.Cryptography
{
    public class Base64 : IBase64
    {
        public string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public string Encode(string plainString)
        {
            var encodedBytes = Encoding.UTF8.GetBytes(plainString);
            return Convert.ToBase64String(encodedBytes);
        }

        public string Decode(string encodedString)
        {
            var decodedBytes = Convert.FromBase64String(encodedString);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        public byte[] DecodeToArray(string encodedString)
        {
            return Convert.FromBase64String(encodedString);
        }

        public string Encode(string plainString, bool trimEqualSigns)
        {
            if (!trimEqualSigns)
            {
                return Encode(plainString);
            }

            var result = Encode(plainString);
            return result.TrimEnd('=');
        }
    }
}