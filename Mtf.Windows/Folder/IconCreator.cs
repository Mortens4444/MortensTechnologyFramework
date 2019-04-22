using System.IO;
using System.Text;
using Mtf.File.Write;

namespace Mtf.Windows.Folder
{
    public class IconCreator
    {
        public void SetDirectoryIcon(string iconFile, string directory, bool confirmFileOperations = true, bool noSharing = true, int iconIndex = 0, string infoTip = "Created by Mortens")
        {
            var di = new DirectoryInfo(directory);
            di.Attributes = di.Attributes | FileAttributes.System;

            var filename = iconFile.Substring(iconFile.LastIndexOf('\\') + 1); // Path.GetFileName(iconFile)
            var icoFilePath = Path.Combine(directory, filename);
            if (!System.IO.File.Exists(icoFilePath))
            {
                System.IO.File.Copy(iconFile, icoFilePath);
            }

            var desktopIniPath = Path.Combine(directory, "desktop.ini");
            if (System.IO.File.Exists(desktopIniPath))
            {
                return;
            }

            var dektopIniContent = new StringBuilder();
            dektopIniContent.AppendLine("[.ShellClassInfo]");
            dektopIniContent.AppendLine();
            var confirmFileOpValue = confirmFileOperations ? "1" : "0";
            dektopIniContent.AppendLine($"ConfirmFileOp={confirmFileOpValue}");
            var noSharingValue = noSharing ? "1" : "0";
            dektopIniContent.AppendLine($"NoSharing={noSharingValue}");
            dektopIniContent.AppendLine($"IconFile={filename}");
            dektopIniContent.AppendLine($"IconIndex={iconIndex}");
            dektopIniContent.Append($"InfoTip={infoTip}");

            var fileCreator = new FileCreator();
            fileCreator.CreateNewFile(desktopIniPath, dektopIniContent.ToString());
            var fileInfo = new FileInfo(desktopIniPath);
            fileInfo.Attributes = fileInfo.Attributes | FileAttributes.Hidden;
        }
    }
}