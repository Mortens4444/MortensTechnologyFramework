using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mtf.File
{
    public static class Utils
    {
        private const int FileBuffer = 4206592;

        private static readonly string[] textfileExtensions =
            { ".asm", ".bat", ".c", ".c++", ".cpp", ".cs", ".css", ".htm", ".html", ".html5", ".ini",
              ".less", ".log", ".lst", ".nfo", ".pas", ".rtf", ".sms", ".srt", ".text", ".txt", ".xml" };

        public static bool IsTextFileExtension(string filename)
        {
            var extension = Path.GetExtension(filename);
            if (extension != null)
            {
                var ext = extension.ToLower();
                return textfileExtensions.Any(textfileExtension => textfileExtension == ext);
            }
            return false;
        }

        public static bool IsTextFile(string filename)
        {
            Encoding encoding;
            return IsTextFile(filename, out encoding);
        }

        public static bool IsTextFile(string filename, out Encoding encoding)
        {
            var result = true;
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var data = new byte[FileBuffer];
                var readBytes = fs.Read(data, 0, data.Length);

                fs.Seek(0, SeekOrigin.Begin);
                if (data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
                {
                    encoding = Encoding.UTF8;
                }
                else if (data[0] == 0xFE && data[1] == 0xFF)
                {
                    encoding = Encoding.Unicode;
                }
                else if (data[0] == 0 && data[1] == 0 && data[2] == 0xFE && data[3] == 0xFF)
                {
                    encoding = Encoding.UTF32;
                }
                else if (data[0] == 0x2B && data[1] == 0x2F && data[2] == 0x76)
                {
                    encoding = Encoding.UTF7;
                }
                else
                {
                    encoding = Encoding.Default;
                }

                var chars = new char[FileBuffer];
                using (var sr = new StreamReader(fs))
                {
                    sr.Read(chars, 0, chars.Length);
                }

                using (var ms = new MemoryStream())
                {
                    using (var sw = new StreamWriter(ms, encoding))
                    {
                        sw.Write(chars);
                        sw.Flush();

                        var buffer = ms.GetBuffer();

                        var i = 0;
                        while (i < readBytes)
                        {
                            if (data[i] != buffer[i])
                            {
                                result = false;
                                break;
                            }
                            i++;
                        }
                    }
                }
            }
            return result;
        }

        public static List<string> Search(string directory, string filename)
        {
            var result = new List<string>();
            try
            {
                result = Directory.GetFiles(directory, filename, SearchOption.TopDirectoryOnly).ToList();
                var subDirectories = Directory.GetDirectories(directory/*, "*", SearchOption.TopDirectoryOnly*/);
                foreach (var subDirectory in subDirectories)
                {
                    var found = Search(subDirectory, filename);
                    if (found.Count <= 0)
                    {
                        continue;
                    }

                    result.AddRange(found);
                }
            }
            catch { }
            return result;
        }

        public static List<string> SearchForFirst(string directory, string filename)
        {
            var result = new List<string>();
            try
            {
                result = Directory.GetFiles(directory, filename, SearchOption.TopDirectoryOnly).ToList();
                if (result.Count > 0)
                {
                    return result;
                }
                var subDirectories = Directory.GetDirectories(directory/*, "*", SearchOption.TopDirectoryOnly*/);
                foreach (var subDirectory in subDirectories)
                {
                    var found = SearchForFirst(subDirectory, filename);
                    if (found.Count <= 0)
                    {
                        continue;
                    }

                    result.AddRange(found);
                    return result;
                }
            }
            catch { }
            return result;
        }
    }
}