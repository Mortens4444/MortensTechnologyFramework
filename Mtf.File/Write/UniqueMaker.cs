using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mtf.File.Write
{
    public class UniqueMaker
    {
        public void MakeUnique(string inputFile, string outputFile, bool sort = false)
        {
            MakeUnique(inputFile, outputFile, Encoding.Default, sort);
        }

        public void MakeUnique(string inputFile, string outputFile, Encoding encoding, bool sort = false)
        {
            var lines = new List<string>();
            using (var fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, encoding))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!lines.Contains(line))
                        {
                            lines.Add(line);
                        }
                    }
                    sr.Close();
                }
                fs.Close();
            }
            if (sort)
            {
                lines.Sort();
            }
            using (var fs = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {
                using (var sw = new StreamWriter(fs, encoding))
                {
                    foreach (var t in lines)
                    {
                        sw.WriteLine(t);
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}