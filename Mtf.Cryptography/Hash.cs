using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mtf.Utils.CharExtensions;
using Mtf.Utils.StringExtensions;

namespace Mtf.Cryptography
{
    // TODO: Rename functions according to coding style
    public class Hash
    {
        /// <summary>
        /// Creates MD5 hash (128 bit - 16 byte)
        /// </summary>
        /// <param name="input">The string we want to hash</param>
        /// <returns>MD5 hashcode</returns>
        public static string MD5_Hash(string input)
        {
            return CreateHash(MD5.Create(), input);
        }

        /// <summary>
        /// Creates SHA-1 hash (160 bit - 20 byte)
        /// </summary>
        /// <param name="input">The string we want to hash</param>
        /// <returns>SHA-1 hashcode</returns>
        public static string SHA1_Hash(string input)
        {
            return CreateHash(new SHA1CryptoServiceProvider(), input);
        }

        /// <summary>
        /// Creates SHA-2 hash (512 bit - 64 byte)
        /// </summary>
        /// <param name="input">The string we want to hash</param>
        /// <returns>SHA-2 hashcode</returns>
        public static string SHA2_Hash(string input)
        {
            return CreateHash(new SHA512Managed(), input);
        }

        public static string CreateHash(HashAlgorithm hashAlgorithm, string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            var sb = new StringBuilder();
            var hashBytes = hashAlgorithm.ComputeHash(Encoding.Default.GetBytes(input));
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string MD5_FileHash(string filePath)
        {
            return CreateHash(filePath, new MD5CryptoServiceProvider());
        }

        public static string SHA1_FileHash(string filePath)
        {
            return CreateHash(filePath, new SHA1CryptoServiceProvider());
        }

        public static string SHA2_FileHash(string filePath)
        {
            return CreateHash(filePath, new SHA512Managed());
        }

        private static string CreateHash(string filePath, HashAlgorithm hashAlgorithm)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}", filePath);
            }
            string result;
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Position = 0;
                var hash = hashAlgorithm.ComputeHash(file);
                result = BitConverter.ToString(hash);
                file.Close();
            }
            return result;
        }

        public static string FindHash(string hash, HashMode mode)
        {
            var str = String.Empty;
            switch (mode)
            {
                case HashMode.MD5:
                    while (MD5_Hash(str) != hash) str = str.GetNext(PasswordCharacters.Visible);
                    break;
                case HashMode.SHA1:
                    while (SHA1_Hash(str) != hash) str = str.GetNext(PasswordCharacters.Visible);
                    break;
                case HashMode.SHA2:
                    while (SHA2_Hash(str) != hash) str = str.GetNext(PasswordCharacters.Visible);
                    break;
            }
            return str;
        }
    }
}