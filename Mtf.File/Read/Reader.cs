using System;
using System.IO;

namespace Mtf.File.Read
{
    public class Reader
    {
        public string[] LoadFile(string filename)
        {
            return LoadFile(filename, Environment.NewLine);
        }

        public string[] LoadFile(string filename, string separator)
        {
            string[] result = null;
            if (System.IO.File.Exists(filename))
            {
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        var content = streamReader.ReadToEnd();
                        streamReader.Close();
                        result = content.Split(new[] { separator }, Int32.MaxValue, StringSplitOptions.None);
                    }
                    fileStream.Close();
                }
            }
            return result;
        }

        public string GetFileContent(string filename)
        {
            string result;
            if (System.IO.File.Exists(filename))
            {
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        result = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    fileStream.Close();
                }
            }
            else
            {
                throw new FileNotFoundException($"File not found: {filename}", filename);
            }
            return result;
        }
    }
}