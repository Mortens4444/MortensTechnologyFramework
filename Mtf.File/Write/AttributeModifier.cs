using System;

namespace Mtf.File.Write
{
    public static class AttributeModifier
    {
        public static void SetCreationDateTime(string file_path, DateTime date)
        {
            System.IO.File.SetCreationTime(file_path, date);
        }

        public static void SetLastAccessDateTime(string file_path, DateTime date)
        {
            System.IO.File.SetLastAccessTime(file_path, date);
        }

        public static void SetModificationDateTime(string file_path, DateTime date)
        {
            System.IO.File.SetLastWriteTime(file_path, date);
        }
    }
}