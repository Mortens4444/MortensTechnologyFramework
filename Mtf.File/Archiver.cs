using System;
using System.IO;
using System.Windows.Forms;
using Mtf.Messages.ConfirmBox;

namespace Mtf.File
{
    public class Archiver
    {
        private const string message = "The target directory already exists. Existing files will be overwritten. Are you sure you want to continue?";

        public void ArchivingDirectoryWithoutSubdirectories(string sourceAndTargetDirectory)
        {
            var now = DateTime.UtcNow;
            var backupDate = $"Backup {now.Year}.{now.Month:D2}.{now.Day:D2}. {now.Hour:D2}{now.Minute:D2}{now.Second:D2}";
            var dackupDestination = String.Concat(sourceAndTargetDirectory, backupDate);
            ArchivingDirectory(sourceAndTargetDirectory, dackupDestination, false, false);
        }

        public void ArchivingDirectory(string source, string target, bool overwriteExisting = true, bool archiveSubdirectories = true)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    var overwrite = overwriteExisting;
                    if (Directory.Exists(target))
                    {
                        if (!overwriteExisting)
                        {
                            overwrite = ConfirmBox.Show("Confirm arhive operation", message, Decide.No) == DialogResult.Yes;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(target);
                    }

                    if (overwrite)
                    {
                        if (target[target.Length - 1] != '\\')
                        {
                            target = String.Concat(target, "\\");
                        }
                        try
                        {
                            foreach (var filename in Directory.GetFiles(source))
                            {
                                System.IO.File.Copy(filename, String.Concat(target, filename.Substring(filename.LastIndexOf('\\'))), true);
                            }
                        }
                        catch { }

                        try
                        {
                            if (archiveSubdirectories)
                            {
                                foreach (var directoryName in Directory.GetDirectories(source))
                                {
                                    var tergetDirectoryName = String.Concat(target, directoryName.Substring(directoryName.LastIndexOf('\\')));
                                    ArchivingDirectory(directoryName, tergetDirectoryName, overwriteExisting);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}