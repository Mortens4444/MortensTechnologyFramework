namespace Mtf.Messages.LoginForm
{
	partial class LoginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mtf.Messages.LoginForm.LoginForm));
			this.p_Main = new System.Windows.Forms.Panel();
			this.gb_Login = new System.Windows.Forms.GroupBox();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_OK = new System.Windows.Forms.Button();
			this.tb_Password = new System.Windows.Forms.TextBox();
			this.lbl_Password = new System.Windows.Forms.Label();
			this.tb_Username = new System.Windows.Forms.TextBox();
			this.lbl_Username = new System.Windows.Forms.Label();
			this.p_Main.SuspendLayout();
			this.gb_Login.SuspendLayout();
			this.SuspendLayout();
			// 
			// p_Main
			// 
			this.p_Main.Controls.Add(this.gb_Login);
			this.p_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Main.Location = new System.Drawing.Point(0, 0);
			this.p_Main.Name = "p_Main";
			this.p_Main.Size = new System.Drawing.Size(383, 137);
			this.p_Main.TabIndex = 0;
			// 
			// gb_Login
			// 
			this.gb_Login.Controls.Add(this.btn_Cancel);
			this.gb_Login.Controls.Add(this.btn_OK);
			this.gb_Login.Controls.Add(this.tb_Password);
			this.gb_Login.Controls.Add(this.lbl_Password);
			this.gb_Login.Controls.Add(this.tb_Username);
			this.gb_Login.Controls.Add(this.lbl_Username);
			this.gb_Login.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gb_Login.Location = new System.Drawing.Point(0, 0);
			this.gb_Login.Name = "gb_Login";
			this.gb_Login.Size = new System.Drawing.Size(383, 137);
			this.gb_Login.TabIndex = 0;
			this.gb_Login.TabStop = false;
			this.gb_Login.Text = "Login";
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(296, 106);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 5;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// btn_OK
			// 
			this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Location = new System.Drawing.Point(215, 106);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(75, 23);
			this.btn_OK.TabIndex = 4;
			this.btn_OK.Text = "OK";
			this.btn_OK.UseVisualStyleBackColor = true;
			// 
			// tb_Password
			// 
			this.tb_Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_Password.Location = new System.Drawing.Point(15, 80);
			this.tb_Password.Name = "tb_Password";
			this.tb_Password.PasswordChar = '*';
			this.tb_Password.Size = new System.Drawing.Size(356, 20);
			this.tb_Password.TabIndex = 3;
			// 
			// lbl_Password
			// 
			this.lbl_Password.AutoSize = true;
			this.lbl_Password.Location = new System.Drawing.Point(12, 64);
			this.lbl_Password.Name = "lbl_Password";
			this.lbl_Password.Size = new System.Drawing.Size(53, 13);
			this.lbl_Password.TabIndex = 2;
			this.lbl_Password.Text = "Password";
			// 
			// tb_Username
			// 
			this.tb_Username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tb_Username.Location = new System.Drawing.Point(15, 38);
			this.tb_Username.Name = "tb_Username";
			this.tb_Username.Size = new System.Drawing.Size(356, 20);
			this.tb_Username.TabIndex = 1;
			// 
			// lbl_Username
			// 
			this.lbl_Username.AutoSize = true;
			this.lbl_Username.Location = new System.Drawing.Point(12, 22);
			this.lbl_Username.Name = "lbl_Username";
			this.lbl_Username.Size = new System.Drawing.Size(55, 13);
			this.lbl_Username.TabIndex = 0;
			this.lbl_Username.Text = "Username";
			// 
			// LoginForm
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(383, 137);
			this.Controls.Add(this.p_Main);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(191, 164);
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			this.p_Main.ResumeLayout(false);
			this.gb_Login.ResumeLayout(false);
			this.gb_Login.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel p_Main;
		private System.Windows.Forms.GroupBox gb_Login;
		private System.Windows.Forms.Label lbl_Username;
		private System.Windows.Forms.TextBox tb_Password;
		private System.Windows.Forms.Label lbl_Password;
		private System.Windows.Forms.TextBox tb_Username;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_OK;
	}
}