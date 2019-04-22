using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mtf.Cryptography.CryptoAlgortihms
{
    public abstract class CryptingBase
    {
        protected ICryptoTransform encryptor;
        protected ICryptoTransform decryptor;

        protected CryptingBase(byte[] key, byte[] initializationVector, params KeyAndInitializationVectorLength[] validKeyAndInitializationVectorLengths)
        {
            if (key != null && initializationVector != null)
            {
                CheckKeyAndInitializationVector(key, initializationVector, validKeyAndInitializationVectorLengths);
            }
        }

        public byte[] Encrypt(char[] originalData)
        {
            byte[] buffer;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(originalData);
                        cryptoStream.FlushFinalBlock();
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    cryptoStream.Close();
                    buffer = memoryStream.ToArray();
                }
                memoryStream.Close();
            }
            return buffer;
        }

        public byte[] Decrypt(byte[] cryptedData)
        {
            if (cryptedData == null)
            {
                return null;
            }

            byte[] result;
            using (var memoryStream = new MemoryStream(cryptedData))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        result = Encoding.UTF8.GetBytes(streamReader.ReadToEnd());
                        streamReader.Close();
                    }
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return result;
        }

        private void CheckKeyAndInitializationVector(byte[] key, byte[] initializationVector, KeyAndInitializationVectorLength[] validKeyAndInitializationVectorLengths)
        {
            var goodKeys = validKeyAndInitializationVectorLengths.Where(validKeyAndInitializationVectorLength => validKeyAndInitializationVectorLength.KeyLength == key.Length);
            if (!goodKeys.Any())
            {
                var validKeys = validKeyAndInitializationVectorLengths.Select(validKeyAndInitializationVectorLength => validKeyAndInitializationVectorLength.KeyLength);
                throw new ArgumentException($"Key size must be [{String.Join(", ", validKeys)}] bytes", nameof(key));
            }
            var goodInitializationVector = goodKeys.Any(goodKey => goodKey.InitializationVectorLength == initializationVector.Length);
            if (!goodInitializationVector)
            {
                var validInitializationVectorLengths = validKeyAndInitializationVectorLengths.Select(validKeyAndInitializationVectorLength => validKeyAndInitializationVectorLength.KeyLength);
                throw new ArgumentException($"Initialization vector size must be [{String.Join(", ", validInitializationVectorLengths)}] bytes", nameof(initializationVector));
            }
        }
    }
}