using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mtf.File.Write
{
    public class FileModifier
    {
        public delegate string StringResultStringParams(string text);

        public void ModifyRows(string inputFile, string outputFile, StringResultStringParams lineConverter)
        {
            ModifyRows(inputFile, outputFile, lineConverter, Encoding.Default);
        }

        public void ModifyRows(string inputFile, string outputFile, StringResultStringParams lineConverter, Encoding encoding)
        {
            var lines = new List<string>();
            using (var fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, encoding))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(lineConverter(line));
                    }
                    sr.Close();
                }
                fs.Close();
            }
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