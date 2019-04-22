using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mtf.File.Write
{
    public class Sorter
    {
        public void Sort(string inputFile, string outputFile)
        {
            Sort(inputFile, outputFile, Encoding.Default);
        }

        public void Sort(string inputFile, string outputFile, Encoding encoding)
        {
            var lines = new List<string>();
            using (var fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, encoding))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    sr.Close();
                }
                fs.Close();
            }
            lines.Sort();
            using (var fs = new FileStream(outputFile, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {
                using (var sw = new StreamWriter(fs, encoding))
                {
                    foreach (var line in lines)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}