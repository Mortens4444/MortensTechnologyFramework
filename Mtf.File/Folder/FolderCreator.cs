using System.IO;

namespace Mtf.File.Folder
{
    public class FolderCreator
    {
        public void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}