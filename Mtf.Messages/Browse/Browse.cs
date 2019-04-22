using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mtf.Messages.Browse
{
	public partial class Browse : Form
	{
		private readonly bool open;
		private readonly string[] extensions;

        private const string parentDirectory = "..";
		private const char folderSeparator = '\\';
	    private const int NotFount = -1;

		public static string FormText = "Browse";
		public static string DriveLabel = "Drive";
		public static string LocationLabel = "Location";
		public static string FilenameLabel = "Filename";
		public static string ButtonOpen = "Open";
		public static string ButtonSave = "Save";
		public static string ButtonCancel = "Cancel";
		public static string ErrorBadCharacterInFilename = "The filename can't contain the following characters" + Environment.NewLine + "\\ /  * ? \" < > |";
		public static string ErrorNoDrivesFound = "No drives found";

	    private static readonly Dictionary<string, Extension> Extensions = new Dictionary<string, Extension>
	    {
	        { ".txt", Extension.txt },
	        { ".wav", Extension.wav },
	        { ".mdf", Extension.mdf },
	        { ".bmp", Extension.bmp },
	        { ".jpg", Extension.jpg },
	        { ".jpeg", Extension.jpeg },
	        { ".xls", Extension.xls },
	        { ".xlsx", Extension.xlsx },
	        { ".c#", Extension.c_sharp },
	        { ".c", Extension.c },
	        { ".c++", Extension.c_plus_plus },
	        { ".cpp", Extension.c_plus_plus },
	        { ".exe", Extension.exe },
	        { ".flv", Extension.flv },
	        { ".fla", Extension.fla },
	        { ".fon", Extension.fon },
	        { ".ttf", Extension.ttf },
	        { ".h", Extension.h },
	        { ".htm", Extension.htm },
	        { ".html", Extension.html },
	        { ".pdf", Extension.pdf },
	        { ".php", Extension.php },
	        { ".ppt", Extension.ppt },
	        { ".doc", Extension.doc },
	        { ".docx", Extension.docx },
	        { ".sql", Extension.sql },
	        { ".png", Extension.png },
	        { ".gif", Extension.gif },
	        { ".ico", Extension.ico },
	        { ".xml", Extension.xml }
	    };

	    public Browse(BrowseType type, params string[] extensions) : this(Directory.GetCurrentDirectory(), type, null, extensions)
		{ }

		public Browse(string startupPath, BrowseType type, params string[] extensions) : this(startupPath, type, null, extensions)
		{ }

		public Browse(string startupPath, BrowseType type, string[] restictedDrives, params string[] extensions)
		{
			InitializeComponent();

			tb_Location.Text = startupPath;
			open = type == BrowseType.Open;
			LoadLanguage();
			if (open)
			{
				tb_Filename.ReadOnly = true;
				tb_Filename.TabStop = false;
			}
			this.extensions = extensions;

			cbDrives.Items.Clear();
			var drives = Directory.GetLogicalDrives();
			foreach (var drive in drives)
			{
			    var found = restictedDrives != null && restictedDrives.Any(restictedDrive => drive == restictedDrive);
			    if (!found)
			    {
			        var drv = new DriveInfo(drive);
			        cbDrives.Items.Add(GetDriveName(drv));
			    }
			}
			if (cbDrives.Items.Count > 0)
			{
			    if (tb_Location.Text == String.Empty)
			    {
			        cbDrives.SelectedIndex = 0;
			    }
				else
				{
					var i = 0;
					while (i < cbDrives.Items.Count)
					{
						if (String.Equals(tb_Location.Text.Substring(0, 2), cbDrives.Items[i].ToString().Substring(0, 2), StringComparison.CurrentCultureIgnoreCase))
						{
							cbDrives.SelectedIndex = i;
							tb_Location.Text = startupPath;
							GetFoldersAndFiles(tb_Location.Text, this.extensions);
							break;
						}
						i++;
					}
				    if (cbDrives.SelectedIndex == NotFount)
				    {
				        cbDrives.SelectedIndex = 0;
				    }
				}
			}
			else tb_Location.Text = ErrorNoDrivesFound;

			lvFiles.Select();
		}

	    private void LoadLanguage()
		{
			Text = FormText;
			lbl_Drive.Text = DriveLabel;
			lbl_Location.Text = LocationLabel;
			lbl_Filename.Text = FilenameLabel;
			btn_Cancel.Text = ButtonCancel;
			btn_OpenSave.Text = open ? ButtonOpen : ButtonSave;
		}

	    private void btn_OpenSave_Click(object sender, EventArgs e)
		{
			tb_Location.Text = Path.Combine(tb_Location.Text, tb_Filename.Text);
			//tb_Location.Text = (tb_Location.Text[tb_Location.Text.Length - 1] != Browse.folderSeparator) ? String.Format("{0}{1}{2}", tb_Location.Text, Browse.folderSeparator, tb_Filename.Text) : String.Format("{0}{1}", tb_Location.Text, tb_Filename.Text);
			//if (tb_Location.Text[tb_Location.Text.Length - 1] != Browse.folderSeparator) tb_Location.Text += Browse.folderSeparator;
			//tb_Location.Text += tb_Filename.Text;

		    if (open || extensions.Length != 1)
		    {
		        return;
		    }

		    if (tb_Location.Text.Length - extensions[0].Length != tb_Location.Text.LastIndexOf(extensions[0]))
		    {
		        tb_Location.Text += $".{extensions[0]}";
		    }
		}

	    private string ChangeDirectory(string location, string changeDirectoryCommand)
		{
			if (changeDirectoryCommand == parentDirectory)
			{
				var directoryInfo = new DirectoryInfo(location);
			    if (directoryInfo.Parent != null)
			    {
			        location = directoryInfo.Parent.FullName;
			    }
			}
			else
			{
				location = Path.Combine(location, changeDirectoryCommand);
//				if (location[location.Length - 1] != Browse.folderSeparator) location += Browse.folderSeparator;
//				location += changeDirectoryCommand;
			}
			tb_Filename.Text = String.Empty;
			return location;
		}

	    private static string GetDriveName(DriveInfo drive)
		{
			try
			{
				return drive.VolumeLabel != String.Empty ? $"{drive.Name} - {drive.VolumeLabel}" : drive.Name;
			}
			catch
			{
			    if (drive != null)
			    {
			        return drive.Name;
			    }
			}
			return String.Empty;
		}

	    private static Extension GetExtensionImageIndex(string extensionWithDot)
		{
		    if (Extensions.ContainsKey(extensionWithDot))
		    {
		        return Extensions[extensionWithDot];
		    }
        	return Extension.all_other;
		}

	    private void GetFoldersAndFiles(string location, IList<string> extensionsList)
		{
			lvFiles.Items.Clear();
			var directoryInfo = new DirectoryInfo(location);
		    if (directoryInfo.Parent != null)
		    {
		        lvFiles.Items.Add(new ListViewItem(parentDirectory, 0));
		    }

		    try
			{
				var directories = directoryInfo.GetDirectories();
			    foreach (var directory in directories)
			    {
			        lvFiles.Items.Add(new ListViewItem(directory.Name, 1));
			    }

				var i = 0;
				while (i < extensionsList.Count)
				{
					var files = directoryInfo.GetFiles($"*.{extensionsList[i]}");
				    foreach (var fileInfo in files)
				    {
				        var extensionImageIndex = (int)GetExtensionImageIndex(fileInfo.Extension);
				        var item = new ListViewItem(fileInfo.Name, extensionImageIndex);
				        lvFiles.Items.Add(item);
				    }
					i++;
				}
			}
			catch { }

		    if (lvFiles.Items.Count > 0)
		    {
		        lvFiles.Items[0].Selected = true;
		        // TODO: Check this code
		        //SelectFirstIfExists();
		    }
			gb_Main.Focus(); // Workaround: Lost focus issues with down arrow key
			lvFiles.Focus();
		}

		public string SelectedFile => tb_Location.Text;

	    private void lv_Files_Click(object sender, EventArgs e)
		{
			GetSelectedFileName();
		}

	    private void GetSelectedFileName()
		{
		    if (lvFiles.SelectedItems.Count != 1)
		    {
		        return;
		    }

			var directoryInfo = new DirectoryInfo(tb_Location.Text);
		    if (lvFiles.SelectedItems[0].Text != parentDirectory)
		    {
		        var files = directoryInfo.GetFiles(lvFiles.SelectedItems[0].Text);
		        if (files.Length == 0)
		        {
		            tb_Filename.Text = String.Empty;
		        }
		        else
		        {
		            foreach (var fileInfo in files)
		            {
		                tb_Filename.Text = fileInfo.Name;
		            }
		        }
		    }
		    else
		    {
		        tb_Filename.Text = String.Empty;
		    }
		}

	    private void lv_Files_DoubleClick(object sender, EventArgs e)
		{
			SelectFileOrOpenFolder();
		}

	    private void SelectFileOrOpenFolder()
		{
			if (lvFiles.SelectedItems.Count != 1)
				return;

			var directoryInfo = new DirectoryInfo(Path.Combine(tb_Location.Text, lvFiles.SelectedItems[0].Text));
			if ((directoryInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
			{
				tb_Location.Text = ChangeDirectory(tb_Location.Text, lvFiles.SelectedItems[0].Text);
				GetFoldersAndFiles(tb_Location.Text, extensions);
			}
			else btn_OpenSave.PerformClick();
		}

	    private void tb_Filename_TextChanged(object sender, EventArgs e)
		{
			if (tb_Filename.Text.Length > 0)
			{
				if (tb_Filename.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
				//if (tb_Filename.Text.IndexOfAny(new char[] { '*', '?', '/', Browse.folderSeparator, ':', '"', '|', '<', '>' }) == -1)
				{
					btn_OpenSave.Enabled = true;
					tt_Tip.Hide(tb_Filename);
				}
				else
				{
					btn_OpenSave.Enabled = false;
					tt_Tip.Show(ErrorBadCharacterInFilename, tb_Filename);
				}
			}
			else btn_OpenSave.Enabled = false;
		}

	    private void cb_Drives_SelectedIndexChanged(object sender, EventArgs e)
		{
		    if (tb_Location.Text[tb_Location.Text.Length - 1] != folderSeparator)
		    {
		        tb_Location.Text = $"{tb_Location.Text}{folderSeparator}";
		    }
		    if (tb_Location.Text.Substring(0, 3) != cbDrives.Text.Substring(0, 3))
		    {
		        tb_Location.Text = cbDrives.Text.Substring(0, 3);
		    }
			GetFoldersAndFiles(tb_Location.Text, extensions);
		}

		public new DialogResult Show()
		{
			return ShowDialog();
		}

		public new DialogResult Show(IWin32Window owner)
		{
			return ShowDialog(owner);
		}

	    private void lv_Files_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case '\b':
					tb_Location.Text = ChangeDirectory(tb_Location.Text, parentDirectory);
					GetFoldersAndFiles(tb_Location.Text, extensions);
					break;
				case '\r':
				case '\n':
					SelectFileOrOpenFolder();
					break;
			}
		}

	    private void lv_Files_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			GetSelectedFileName();
		}
	}
}