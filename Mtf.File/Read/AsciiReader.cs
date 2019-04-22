using System;
using System.IO;
using System.Text;
using Mtf.Messages.ErrorBox;

namespace Mtf.File.Read
{
    public class AsciiReader
    {
        private const string FileNotFound = "File not found: ";

        public string GetAsciiArray(string filename, int numberOfBytesInARow = 10)
        {
            var buffer = new byte[numberOfBytesInARow];
            var sb = new StringBuilder();
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    using (var fs = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        int offset = 0, db;
                        while ((db = fs.Read(buffer, offset, numberOfBytesInARow)) > 0)
                        {
                            offset += db;
                            for (var i = 0; i < db; i++)
                            {
                                sb.Append($"0x{$"{buffer[i]:x2}".ToUpper()}");
                                if (i < numberOfBytesInARow - 1)
                                {
                                    sb.Append(", ");
                                }
                                else
                                {
                                    sb.AppendLine(",");
                                }
                            }
                        }
                        fs.Close();
                    }
                }
                else throw new FileNotFoundException(FileNotFound, filename);
            }
            catch (Exception ex)
            {
                ErrorBox.Show(ex);
            }
            return sb.ToString();
        }

        public string GetFileContentFromAsciiArray(string asciiArray)
        {
            var sb = new StringBuilder();

            var index = 0;
            while ((index = asciiArray.IndexOf("0x", index, StringComparison.Ordinal)) >= 0)
            {
                var min = asciiArray.IndexOf(',', index) - 2 - index;
                if (min > 0)
                {
                    index += min;
                    try
                    {
                        var hexaCh = asciiArray.Substring(index, min);
                        if (hexaCh != "00" && hexaCh != String.Empty)
                        {
                            sb.Append(Convert.ToChar(Convert.ToInt32(hexaCh, 16)));
                        }
                    }
                    catch { }
                }
                else
                {
                    index++;
                }
            }
            return sb.ToString();
        }
    }
}