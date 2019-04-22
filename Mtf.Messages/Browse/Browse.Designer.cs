// TODO: Use custom control
//using Network.Controls;

namespace Mtf.Messages.Browse
{
	partial class Browse
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browse));
			this.p_Main = new System.Windows.Forms.Panel();
			this.gb_Main = new System.Windows.Forms.GroupBox();
			this.lvFiles = new System.Windows.Forms.ListView();//Network.Controls.ListViewNF();
			this.il_FileImages = new System.Windows.Forms.ImageList(this.components);
			this.lbl_Filename = new System.Windows.Forms.Label();
			this.tb_Filename = new System.Windows.Forms.TextBox();
			this.lbl_Drive = new System.Windows.Forms.Label();
			this.cbDrives = new System.Windows.Forms.ComboBox();
			this.tb_Location = new System.Windows.Forms.TextBox();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_OpenSave = new System.Windows.Forms.Button();
			this.lbl_Location = new System.Windows.Forms.Label();
			this.tt_Tip = new System.Windows.Forms.ToolTip(this.components);
			this.p_Main.SuspendLayout();
			this.gb_Main.SuspendLayout();
			this.SuspendLayout();
			// 
			// p_Main
			// 
			this.p_Main.Controls.Add(this.gb_Main);
			this.p_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Main.Location = new System.Drawing.Point(0, 0);
			this.p_Main.Name = "p_Main";
			this.p_Main.Size = new System.Drawing.Size(462, 302);
			this.p_Main.TabIndex = 0;
			// 
			// gb_Main
			// 
			this.gb_Main.Controls.Add(this.lvFiles);
			this.gb_Main.Controls.Add(this.lbl_Filename);
			this.gb_Main.Controls.Add(this.tb_Filename);
			this.gb_Main.Controls.Add(this.lbl_Drive);
			this.gb_Main.Controls.Add(this.cbDrives);
			this.gb_Main.Controls.Add(this.tb_Location);
			this.gb_Main.Controls.Add(this.btn_Cancel);
			this.gb_Main.Controls.Add(this.btn_OpenSave);
			this.gb_Main.Controls.Add(this.lbl_Location);
			this.gb_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb_Main.Location = new System.Drawing.Point(0, 0);
			this.gb_Main.Name = "gb_Main";
			this.gb_Main.Size = new System.Drawing.Size(462, 302);
			this.gb_Main.TabIndex = 0;
			this.gb_Main.TabStop = false;
			// 
			// lvFiles
			// 
			/*this.lvFiles.AlternatingColorEven = System.Drawing.Color.LightBlue;
			this.lvFiles.AlternatingColorOdd = System.Drawing.SystemColors.Window;
			this.lvFiles.AlternatingColorsAreInUse = false;
			this.lvFiles.FirstItemIsGray = false;*/
			this.lvFiles.FullRowSelect = true;
			this.lvFiles.LargeImageList = this.il_FileImages;
			this.lvFiles.Location = new System.Drawing.Point(2, 91);
			this.lvFiles.Name = "lv_Files";
			this.lvFiles.OwnerDraw = true;
			this.lvFiles.Size = new System.Drawing.Size(457, 176);
			this.lvFiles.SmallImageList = this.il_FileImages;
			this.lvFiles.TabIndex = 6;
			this.lvFiles.UseCompatibleStateImageBehavior = false;
			this.lvFiles.View = System.Windows.Forms.View.List;
			this.lvFiles.DoubleClick += new System.EventHandler(this.lv_Files_DoubleClick);
			this.lvFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_Files_ItemSelectionChanged);
			this.lvFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lv_Files_KeyPress);
			this.lvFiles.Click += new System.EventHandler(this.lv_Files_Click);
			// 
			// il_FileImages
			// 
			this.il_FileImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il_FileImages.ImageStream")));
			this.il_FileImages.TransparentColor = System.Drawing.Color.Transparent;
			this.il_FileImages.Images.SetKeyName(0, "up.ico");
			this.il_FileImages.Images.SetKeyName(1, "folder.ico");
			this.il_FileImages.Images.SetKeyName(2, "white_page.ico");
			this.il_FileImages.Images.SetKeyName(3, "text_page.ico");
			this.il_FileImages.Images.SetKeyName(4, "wav.ico");
			this.il_FileImages.Images.SetKeyName(5, "database.ico");
			this.il_FileImages.Images.SetKeyName(6, "bmp.ico");
			this.il_FileImages.Images.SetKeyName(7, "jpg.ico");
			this.il_FileImages.Images.SetKeyName(8, "xls.ico");
			this.il_FileImages.Images.SetKeyName(9, "c#.ico");
			this.il_FileImages.Images.SetKeyName(10, "c.ico");
			this.il_FileImages.Images.SetKeyName(11, "c++.ico");
			this.il_FileImages.Images.SetKeyName(12, "exe.ico");
			this.il_FileImages.Images.SetKeyName(13, "flv_fla.ico");
			this.il_FileImages.Images.SetKeyName(14, "fon.ico");
			this.il_FileImages.Images.SetKeyName(15, "h.ico");
			this.il_FileImages.Images.SetKeyName(16, "htm.ico");
			this.il_FileImages.Images.SetKeyName(17, "pdf.ico");
			this.il_FileImages.Images.SetKeyName(18, "php.ico");
			this.il_FileImages.Images.SetKeyName(19, "ppt.ico");
			this.il_FileImages.Images.SetKeyName(20, "doc.ico");
			this.il_FileImages.Images.SetKeyName(21, "sql.ico");
			this.il_FileImages.Images.SetKeyName(22, "png.ico");
			this.il_FileImages.Images.SetKeyName(23, "gif.ico");
			this.il_FileImages.Images.SetKeyName(24, "ico.ico");
			this.il_FileImages.Images.SetKeyName(25, "xml.ico");
			// 
			// lbl_Filename
			// 
			this.lbl_Filename.AutoSize = true;
			this.lbl_Filename.Location = new System.Drawing.Point(5, 68);
			this.lbl_Filename.Name = "lbl_Filename";
			this.lbl_Filename.Size = new System.Drawing.Size(49, 13);
			this.lbl_Filename.TabIndex = 4;
			this.lbl_Filename.Text = "Filename";
			// 
			// tb_Filename
			// 
			this.tb_Filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_Filename.Location = new System.Drawing.Point(92, 65);
			this.tb_Filename.Name = "tb_Filename";
			this.tb_Filename.Size = new System.Drawing.Size(367, 20);
			this.tb_Filename.TabIndex = 5;
			this.tb_Filename.TextChanged += new System.EventHandler(this.tb_Filename_TextChanged);
			// 
			// lbl_Drive
			// 
			this.lbl_Drive.AutoSize = true;
			this.lbl_Drive.Location = new System.Drawing.Point(5, 15);
			this.lbl_Drive.Name = "lbl_Drive";
			this.lbl_Drive.Size = new System.Drawing.Size(32, 13);
			this.lbl_Drive.TabIndex = 0;
			this.lbl_Drive.Text = "Drive";
			// 
			// cbDrives
			// 
			this.cbDrives.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDrives.FormattingEnabled = true;
			this.cbDrives.Location = new System.Drawing.Point(92, 12);
			this.cbDrives.Name = "cbDrives";
			this.cbDrives.Size = new System.Drawing.Size(367, 21);
			this.cbDrives.TabIndex = 1;
			this.cbDrives.SelectedIndexChanged += new System.EventHandler(this.cb_Drives_SelectedIndexChanged);
			// 
			// tb_Location
			// 
			this.tb_Location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_Location.Location = new System.Drawing.Point(92, 39);
			this.tb_Location.MaxLength = 1000;
			this.tb_Location.Name = "tb_Location";
			this.tb_Location.ReadOnly = true;
			this.tb_Location.Size = new System.Drawing.Size(367, 20);
			this.tb_Location.TabIndex = 3;
			this.tb_Location.TabStop = false;
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(381, 273);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 8;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// btn_OpenSave
			// 
			this.btn_OpenSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_OpenSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OpenSave.Enabled = false;
			this.btn_OpenSave.Location = new System.Drawing.Point(300, 273);
			this.btn_OpenSave.Name = "btn_OpenSave";
			this.btn_OpenSave.Size = new System.Drawing.Size(75, 23);
			this.btn_OpenSave.TabIndex = 7;
			this.btn_OpenSave.Text = "Open/Save";
			this.btn_OpenSave.UseVisualStyleBackColor = true;
			this.btn_OpenSave.Click += new System.EventHandler(this.btn_OpenSave_Click);
			// 
			// lbl_Location
			// 
			this.lbl_Location.AutoSize = true;
			this.lbl_Location.Location = new System.Drawing.Point(5, 42);
			this.lbl_Location.Name = "lbl_Location";
			this.lbl_Location.Size = new System.Drawing.Size(48, 13);
			this.lbl_Location.TabIndex = 2;
			this.lbl_Location.Text = "Location";
			// 
			// tt_Tip
			// 
			this.tt_Tip.AutoPopDelay = 5000;
			this.tt_Tip.InitialDelay = 500;
			this.tt_Tip.ReshowDelay = 5000;
			// 
			// Browse
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(462, 302);
			this.Controls.Add(this.p_Main);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Browse";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Browse";
			this.TopMost = true;
			this.p_Main.ResumeLayout(false);
			this.gb_Main.ResumeLayout(false);
			this.gb_Main.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel p_Main;
		private System.Windows.Forms.GroupBox gb_Main;
		private System.Windows.Forms.Label lbl_Filename;
		private System.Windows.Forms.TextBox tb_Filename;
		private System.Windows.Forms.Label lbl_Drive;
		private System.Windows.Forms.ComboBox cbDrives;
		private System.Windows.Forms.TextBox tb_Location;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_OpenSave;
		private System.Windows.Forms.Label lbl_Location;
		private System.Windows.Forms.ListView lvFiles;//ListViewNF lvFiles;
		private System.Windows.Forms.ImageList il_FileImages;
		private System.Windows.Forms.ToolTip tt_Tip;
	}
}