namespace Mtf.File.Write
{
    public class FileCreator
    {
        /// <summary>
        /// Overwrites existing file, with StreamWriter.WriteLine
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public void CreateNewFileWithNewLine(string filename, string data)
        {
            using (var sw = System.IO.File.CreateText(filename))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }

        /// <summary>
        /// Overwrites existing file, with StreamWriter.Write
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public void CreateNewFile(string filename, string data)
        {
            using (var sw = System.IO.File.CreateText(filename))
            {
                sw.Write(data);
                sw.Close();
            }
        }

        /// <summary>
        /// Append content to file if exsits, otherwise creates a new one, with StreamWriter.WriteLine
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public void CreateOrAppendToFileWithNewLine(string filename, string data)
        {
            using (var sw = System.IO.File.AppendText(filename))
            {
                sw.WriteLine(data);
                sw.Close();
            }

        }

        /// <summary>
        /// Append content to file if exsits, otherwise creates a new one, with StreamWriter.Write
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public void CreateOrAppendToFile(string filename, string data)
        {
            using (var sw = System.IO.File.AppendText(filename))
            {
                sw.Write(data);
                sw.Close();
            }
        }
    }
}