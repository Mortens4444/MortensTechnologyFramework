using System.IO;

namespace Mtf.File.Folder
{
    public static class AttributeModifier
    {
        public static bool SetSystemDirectory(string directory)
        {
            try
            {
                var di = new DirectoryInfo(directory);
                di.Attributes = di.Attributes | FileAttributes.System;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}