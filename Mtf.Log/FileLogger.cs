using System;
using System.IO;
using System.Text;

namespace Mtf.Log
{
    public class FileLogger
    {
        private readonly string fullFilename;
        private readonly bool showdate;

        public FileLogger(string folder, string filename, bool showdate = false)
        {
            CreateFolderIfNotExists(folder);
            fullFilename = Path.Combine(folder, filename);
            this.showdate = showdate;
        }

        public void Log(params string[] loginfos)
        {
            var logData = new StringBuilder();
            if (showdate)
            {
                var now = DateTime.UtcNow;
                logData.AppendLine("______________________________________________________________________________________________________________________");
                logData.AppendLine($"\t\t\t\t\tDate: {now.ToShortDateString()} {now.ToLongTimeString()}");
                logData.AppendLine("______________________________________________________________________________________________________________________");
            }
            foreach (var loginfo in loginfos)
            {
                logData.AppendLine(loginfo);
            }

            WriteToTextFile(logData.ToString());
        }

        private static void CreateFolderIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void WriteToTextFile(string data)
        {
            using (var sw = File.AppendText(fullFilename))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }
    }
}