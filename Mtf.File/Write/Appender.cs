using System;

namespace Mtf.File.Write
{
    public static class Appender
    {
        public static void AppendToFile(string filePath, string lineToAppend)
        {
            using (var sw = System.IO.File.AppendText(filePath))
            {
                sw.Write(lineToAppend);
                sw.Flush();
                sw.Close();
            }
        }

        public static void AppendLineToFile(string filePath, string lineToAppend = null)
        {
            using (var sw = System.IO.File.AppendText(filePath))
            {
                if (String.IsNullOrEmpty(lineToAppend))
                {
                    sw.WriteLine();
                }
                else
                {
                    sw.WriteLine(lineToAppend);
                }
                sw.Flush();
                sw.Close();
            }
        }
    }
}