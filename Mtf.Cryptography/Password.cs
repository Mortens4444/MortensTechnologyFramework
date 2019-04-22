using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Mtf.Utils;
using Mtf.Utils.CharExtensions;
using Mtf.Utils.StringExtensions;

namespace Mtf.Cryptography
{
    public class Password
    {
        public static byte[] GetPassword(string password, byte[] salt, byte[] initializationVector, string chiperAlgorithmName, string hashAlgorithmNameToDeriveKey, int keySize)
        {
            var pdb = new PasswordDeriveBytes(password, salt);
            return pdb.CryptDeriveKey(chiperAlgorithmName, hashAlgorithmNameToDeriveKey, keySize, initializationVector);
        }

        public static char GenerateWordLikeChar()
        {
            return GenerateFromChars(PasswordCharacters.Vowels, PasswordCharacters.Consonant);
        }

        public static char GenerateEnglishWordLikeChar()
        {
            return GenerateFromChars(PasswordCharacters.EnglishVowels, PasswordCharacters.Consonant);
        }

        public static char GenerateEnglishVowel()
        {
            return GenerateFromChars(PasswordCharacters.EnglishVowels);
        }

        public static char GenerateVowel()
        {
            return GenerateFromChars(PasswordCharacters.Vowels);
        }

        public static char GenerateConsonant()
        {
            return GenerateFromChars(PasswordCharacters.Consonant);
        }

        public static string GenerateEnglishWordLikePassword()
        {
            return GenerateEnglishWordLikePassword(8, 12);
        }

        public static string GenerateHungarianWordLikePassword()
        {
            return GenerateHungarianWordLikePassword(8, 12);
        }

        public static string GenerateHungarianWordLikePassword(int minLength, int maxLength)
        {
            var length = GetRandomNumber(minLength, maxLength);
            var result = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                result.Append(Utils.CharExtensions.BaseExtensions.GenerateCharByHungarianStatistics());
            }

            return result.ToString();
        }

        public static string GenerateWordLikePassword()
        {
            return GenerateWordLikePassword(8, 12);
        }

        public static string GenerateEnglishWordLikePassword(int minLength, int maxLength)
        {
            return PasswordGenerator(minLength, maxLength, GenerateEnglishWordLikeChar, GenerateEnglishVowel, GenerateConsonant);
        }

        public static string GenerateWordLikePassword(int minLength, int maxLength)
        {
            return PasswordGenerator(minLength, maxLength, GenerateWordLikeChar, GenerateVowel, GenerateConsonant);
        }

        public static string GenerateCodedEnglishWordLikePassword()
        {
            return GenerateEnglishWordLikePassword().GetCodedString();
        }

        public static string GenerateCodedWordLikePassword()
        {
            return GenerateWordLikePassword().GetCodedString();
        }

        public static string GeneratePassword()
        {
            return GeneratePassword(8, 12);
        }

        public static string GeneratePassword(int minimumLength, int maximumLength)
        {
            var result = new StringBuilder();
            var randomNumberGenerator = GetRandomNumberGenerator();
            var passwordLength = randomNumberGenerator.Next(minimumLength, maximumLength + 1);

            for (var i = 0; i < passwordLength; i++)
                result.Append(Convert.ToChar(randomNumberGenerator.Next(0, 255)));

            return result.ToString();
        }

        public static string GeneratePassword(int minimumLength, int maximumLength, char[] chars)
        {
            var result = new StringBuilder();
            var randomNumberGenerator = GetRandomNumberGenerator();
            var passwordLength = randomNumberGenerator.Next(minimumLength, maximumLength + 1);

            for (var i = 0; i < passwordLength; i++)
                result.Append(chars[randomNumberGenerator.Next(0, chars.Length)]);

            return result.ToString();
        }

        private delegate char CharGenerator();

        private static string PasswordGenerator(int minLength, int maxLength,
            CharGenerator charGenerator, CharGenerator vowelGenerator, CharGenerator consonantGenerator)
        {
            var length = GetRandomNumber(minLength, maxLength);
            var result = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                switch (result.Length)
                {
                    case 0:
                    case 1:
                        result.Append(charGenerator());
                        break;
                    default:
                        if (result[result.Length - 1].IsConsonant() && result[result.Length - 2].IsConsonant())
                        {
                            result.Append(vowelGenerator());
                        }
                        else if (result[result.Length - 1].IsVowel() /*&& result[result.Length - 2].IsVowel()*/)
                        {
                            result.Append(consonantGenerator());
                        }
                        else
                        {
                            result.Append(charGenerator());
                        }
                        break;
                }

            }
            return result.ToString();
        }

        private static char GenerateFromChars(IReadOnlyList<char> chars)
        {
            var chosen = GetRandomNumber(chars.Count);
            return chars[chosen];
        }

        private static Random GetRandomNumberGenerator()
        {
            var seed = RandomUtils.GetSeed();
            return new Random(seed);
        }

        private static int GetRandomNumber(int maxValue, int minValue = 0)
        {
            var randomNumberGenerator = GetRandomNumberGenerator();
            return randomNumberGenerator.Next(minValue, maxValue);
        }

        private static char GenerateFromChars(IReadOnlyList<char> chars1, IReadOnlyList<char> chars2)
        {
            return GenerateFromChars(Environment.TickCount % 2 == 0 ? chars1 : chars2);
        }
    }
}