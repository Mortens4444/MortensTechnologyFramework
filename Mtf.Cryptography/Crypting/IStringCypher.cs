namespace Mtf.Cryptography.Crypting
{
    public interface IStringCypher
    {
        string Encrypt(string plainText);

        string Decrypt(string cryptedText);
    }
}