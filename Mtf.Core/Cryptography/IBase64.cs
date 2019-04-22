namespace Mtf.Core.Cryptography
{
    public interface IBase64
    {
        string Encode(byte[] bytes);

        string Encode(string plainString);

        string Decode(string encodedString);

        byte[] DecodeToArray(string encodedString);

        string Encode(string plainString, bool trimEqualSigns);
    }
}