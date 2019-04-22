using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mtf.File.Read
{
    public class WordReader
    {
        public List<string> GetWordsFromFile(string inputFile)
        {
            return GetWordsFromFile(inputFile, Encoding.Default);
        }

        public List<string> GetWordsFromFile(string inputFile, Encoding encoding)
        {
            var result = new List<string>();
            using (var fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, encoding))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var words = line.Split(' ');
                        foreach (var word in words)
                        {
                            if (!result.Contains(word))
                            {
                                result.Add(word);
                            }
                        }
                    }
                    sr.Close();
                }
                fs.Close();
            }
            return result;
        }

        public void GetWordsFromFile(string inputFile, string outputFile)
        {
            var allWords = GetWordsFromFile(inputFile);
            allWords.Sort();

            using (var fs = new FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (var sw = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (var word in allWords)
                    {
                        sw.WriteLine(word);
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }
    }
}