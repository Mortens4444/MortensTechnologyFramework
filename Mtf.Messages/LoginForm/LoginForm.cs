using System.Windows.Forms;

namespace Mtf.Messages.LoginForm
{
	public partial class LoginForm : Form
	{
		public static string Login = "Login";
		public static string UsernameLabel = "Username";
		public static string PasswordLabel = "Password";
		public static string Ok = "OK";
		public static string Cancel = "Cancel";

		public LoginForm(string text)
		{
			InitializeComponent();
			Text = text;
			gb_Login.Text = Login;
			tb_Username.Text = UsernameLabel;
			tb_Password.Text = PasswordLabel;
			btn_OK.Text = Ok;
			btn_Cancel.Text = Cancel;
		}

	    public sealed override string Text
	    {
	        get { return base.Text; }
	        set { base.Text = value; }
	    }

	    public string Username => tb_Username.Text;

	    public string Password => tb_Password.Text;
	}
}
