using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using Mtf.Utils.CharExtensions;

namespace Mtf.Utils.StringExtensions
{
    public static class Cryptography
    {
        public static bool IsStrongPassword(this string value)
        {
            return value.Length > 8 && value.IsContainsSpecialLetterAndDigit() && value.IsContainsLowerLetter() && value.IsContainsUpperLetter();
        }

        public static string GetCodedString(this string value)
        {
            var result = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
            {
                result.Append(value[i].GetCodedChar());
            }
            return result.ToString();
        }

        // TODO: Should be deleted or placed into Cryptography module
        /*public static string MD5(this string value)
        {
            return Hash.MD5_Hash(value);
        }

        public static string SHA1(this string value)
        {
            return Hash.SHA1_Hash(value);
        }

        public static string SHA2(this string value)
        {
            return Hash.SHA2_Hash(value);
        }

        public static string Base64Encode(this string value)
        {
            return Base64.Encode(value);
        }

        public static string Base64Decode(this string value)
        {
            return Base64.Decode(value);
        }

        public static string XorCrypt(this string value, string password)
        {
            return SimpleCypher.XorCypher(value, password);
        }

        public static string CaesarEncrypt(this string value, int key)
        {
            return SimpleCypher.ReplaceCypher(value, key);
        }

        public static string CaesarDecrypt(this string value, int key)
        {
            return SimpleCypher.ReplaceCypher(value, -key);
        }

        public static string DES_Encrypt(this string value)
        {
            return Base64.Encode(DES.DES_Encrypt(UTF8Encoding.UTF8.GetChars(UTF8Encoding.UTF8.GetBytes(value)), DES.DefaultPassword, DES.DefaultPassword));
        }

        public static string DES_Decrypt(this string value)
        {
            return UTF8Encoding.UTF8.GetString(DES.DES_Decrypt(Base64.DecodeToArray(value), DES.DefaultPassword, DES.DefaultPassword));
        }

        public static string DES_Encrypt(this string value, string password)
        {
            var pass_bytes = Encoding.ASCII.GetBytes(password);
            return Base64.Encode(DES.DES_Encrypt(Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(value)), pass_bytes, pass_bytes));
        }

        public static string DES_Decrypt(this string value, string password)
        {
            var pass_bytes = Encoding.ASCII.GetBytes(password);
            return Encoding.UTF8.GetString(DES.DES_Decrypt(Base64.DecodeToArray(value), pass_bytes, pass_bytes));
        }

        public static string DES_Encrypt(this string value, string password, string iv)
        {
            return Base64.Encode(DES.DES_Encrypt(Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(value)), Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv)));
        }

        public static string DES_Decrypt(this string value, string password, string iv)
        {
            return Encoding.UTF8.GetString(DES.DES_Decrypt(Base64.DecodeToArray(value), Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv)));
        }

        public static string TripleDES_Encrypt(this string value, string password)
        {
            var pass_bytes = Encoding.ASCII.GetBytes(password);
            return Base64.Encode(TripleDES.TripleDES_Encrypt(Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(value)), pass_bytes, pass_bytes));
        }

        public static string TripleDES_Decrypt(this string value, string password)
        {
            var pass_bytes = Encoding.ASCII.GetBytes(password);
            return Encoding.UTF8.GetString(TripleDES.TripleDES_Decrypt(Base64.DecodeToArray(value), pass_bytes, pass_bytes));
        }

        public static string TripleDES_Encrypt(this string value, string password, string iv)
        {
            return Base64.Encode(TripleDES.TripleDES_Encrypt(Encoding.UTF8.GetChars(Encoding.UTF8.GetBytes(value)), Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv)));
        }

        public static string TripleDES_Encrypt(this string value, byte[] password, byte[] iv)
        {
            return TripleDES.TripleDES_Encrypt(value, password, iv);
            //return Base64.Encode(TripleDES.TripleDES_Encrypt(UTF8Encoding.UTF8.GetChars(UTF8Encoding.UTF8.GetBytes(value)), password, iv));
        }

        public static string TripleDES_EncryptToArray(this string value, string password, string iv)
        {
            //string result = "";
            var bytes = Encoding.UTF8.GetBytes(value);
            var encrypted = TripleDES.TripleDES_Encrypt(Encoding.UTF8.GetChars(bytes), Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv));
            return encrypted.ToArrayString();
            //for (int i = 0; i < encrypted.Length; i++) result += "[" + encrypted[i].ToString() + "]";
            //return result;
        }

        public static string TripleDES_DecryptFromArray(this string value, string password, string iv)
        {
            var bytes = ArrayStringToArray(value);
            //string[] byte_strings = value.Split(new char[] { '[', ']' });
            //byte[] bytes = new byte[byte_strings.Length / 2];
            //for (int i = 0; i < bytes.Length; i++) bytes[i] = Convert.ToByte(byte_strings[2 * i + 1]);
            var decrypted = TripleDES.TripleDES_Decrypt(bytes, Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv));
            return Encoding.UTF8.GetString(decrypted);
        }

        public static string TripleDES_Decrypt(this string value, string password, string iv)
        {
            return Encoding.UTF8.GetString(TripleDES.TripleDES_Decrypt(Base64.DecodeToArray(value), Encoding.ASCII.GetBytes(password), Encoding.ASCII.GetBytes(iv)));
        }

        public static string TripleDES_Decrypt(this string value, byte[] password, byte[] iv)
        {
            return TripleDES.TripleDES_Decrypt(value, password, iv);
            //return UTF8Encoding.UTF8.GetString(TripleDES.TripleDES_Decrypt(Base64.DecodeToArray(value), password, iv));
        }

        public static string RotateEncrypt(this string value, int key)
        {
            return SimpleCypher.RotateCypher(value, key);
        }

        public static string RotateDecrypt(this string value, int key)
        {
            return SimpleCypher.RotateCypher(value, -key);
        }*/
    }
}