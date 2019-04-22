namespace Mtf.Messages.WaitForm
{
	partial class WaitForm
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
			this.p_Main = new System.Windows.Forms.Panel();
			this.gb_Main = new System.Windows.Forms.GroupBox();
			this.pb_Progress = new System.Windows.Forms.ProgressBar();
			this.l_WaitMessage = new System.Windows.Forms.Label();
			this.bw_Refresh = new System.ComponentModel.BackgroundWorker();
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
			this.p_Main.Size = new System.Drawing.Size(307, 67);
			this.p_Main.TabIndex = 0;
			// 
			// gb_Main
			// 
			this.gb_Main.Controls.Add(this.pb_Progress);
			this.gb_Main.Controls.Add(this.l_WaitMessage);
			this.gb_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb_Main.Location = new System.Drawing.Point(0, 0);
			this.gb_Main.Name = "gb_Main";
			this.gb_Main.Size = new System.Drawing.Size(307, 67);
			this.gb_Main.TabIndex = 0;
			this.gb_Main.TabStop = false;
			// 
			// pb_Progress
			// 
			this.pb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pb_Progress.Location = new System.Drawing.Point(6, 34);
			this.pb_Progress.Name = "pb_Progress";
			this.pb_Progress.Size = new System.Drawing.Size(295, 23);
			this.pb_Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.pb_Progress.TabIndex = 1;
			// 
			// l_WaitMessage
			// 
			this.l_WaitMessage.AutoSize = true;
			this.l_WaitMessage.Location = new System.Drawing.Point(29, 16);
			this.l_WaitMessage.Name = "l_WaitMessage";
			this.l_WaitMessage.Size = new System.Drawing.Size(243, 13);
			this.l_WaitMessage.TabIndex = 0;
			this.l_WaitMessage.Text = "Please wait... This operation could take some time";
			// 
			// bw_Refresh
			// 
			this.bw_Refresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_Refresh_DoWork);
			// 
			// WaitForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(307, 67);
			this.Controls.Add(this.p_Main);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "WaitForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Please wait";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.WaitForm_Deactivate);
			this.Shown += new System.EventHandler(this.WaitForm_Shown);
			this.p_Main.ResumeLayout(false);
			this.gb_Main.ResumeLayout(false);
			this.gb_Main.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel p_Main;
		private System.Windows.Forms.GroupBox gb_Main;
		private System.Windows.Forms.ProgressBar pb_Progress;
		private System.Windows.Forms.Label l_WaitMessage;
		private System.ComponentModel.BackgroundWorker bw_Refresh;
	}
}