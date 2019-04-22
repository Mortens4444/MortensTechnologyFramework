using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Mtf.Messages.ConfirmBox
{
	public partial class ConfirmBox : BaseBox.BaseBox
	{
		readonly bool defaultChoose;
		readonly bool showAutocloseButtons;

		protected ConfirmBox() { }

		protected ConfirmBox(string title, string message, int intervalInMilliseconds, Decide defaultChoose)
		{
			InitializeComponent();
			btn_Yes.Text = Yes;
			btn_No.Text = No;
			Text = String.Concat(Application.ProductName, ": ", title);
			rtb_Message.Text = message;
			t_Close.Enabled = false;
			this.defaultChoose = Decide.Yes == defaultChoose;
			
			showAutocloseButtons = intervalInMilliseconds != Timeout.Infinite;
		    if (showAutocloseButtons)
		    {
		        t_Close.Interval = intervalInMilliseconds;
		    }
			t_DecrementSecondsLeft.Enabled = false;
		}

		public sealed override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

	    private void FocusAcceptButton()
		{
			if (defaultChoose) btn_Yes.Focus();
			else btn_No.Focus();
		}

	    private void btn_Pin_Click(object sender, EventArgs e)
		{
			PinMessage();
		}

	    private void btn_Unpin_Click(object sender, EventArgs e)
		{
			UnpinMessage();
		}

	    private void PinMessage()
		{
			t_Close.Stop();
			t_DecrementSecondsLeft.Stop();
			btn_Pin.Visible = false;
			btn_Unpin.Visible = showAutocloseButtons;
			tt_Hint.SetToolTip(btn_Unpin, EnableAutomaticMessageClosing);
			btn_Yes.Text = Yes;
			btn_No.Text = No;
			FocusAcceptButton();
		}

	    private void UnpinMessage()
		{
			btn_Pin.Visible = true;
			btn_Unpin.Visible = false;
			tt_Hint.SetToolTip(btn_Pin, DisableAutomaticMessageClosing);
			t_Close.Start();
			t_DecrementSecondsLeft.Start();
			SecondsLeft = (int)Math.Truncate((decimal)t_Close.Interval / 1000);
			ShowMessageOnDefaultButton();
		}

	    private void ShowMessageOnDefaultButton()
		{
			var okSecondsLeft = new StringBuilder();
			okSecondsLeft.Append(defaultChoose ? Yes : No);
			okSecondsLeft.AppendFormat(" ({0})", SecondsLeft);
		    var buuton = defaultChoose ? btn_Yes : btn_No;
		    buuton.Text = okSecondsLeft.ToString();
		}

	    private void t_DecrementSecondsLeft_Tick(object sender, EventArgs e)
		{
			SecondsLeft--;
			ShowMessageOnDefaultButton();
		}

	    private static DialogResult Show(ConfirmBox cb)
		{
		    if (cb.ParentWindow != null)
		    {
		        cb.Left = cb.ParentWindow.Left + (cb.ParentWindow.Width - cb.Width) / 2;
		        cb.Top = cb.ParentWindow.Top + (cb.ParentWindow.Height - cb.Height) / 2;
		    }
		    else
		    {
		        cb.StartPosition = FormStartPosition.CenterScreen;
		    }
			return cb.ShowDialog();
		}

		public static DialogResult Show(string title, string message, int intervalInMilliseconds, Decide defaultChoose)
		{
			return Show(null, title, message, intervalInMilliseconds, defaultChoose);
		}

		/*public static bool Show(string title, string message, Decide defaultChoose)
		{
			return Show(null, title, message, Timeout.Infinite, defaultChoose) == DialogResult.Yes;
		}*/

		public static DialogResult Show(string title, string message, Decide defaultChoose)
		{
			return Show(null, title, message, Timeout.Infinite, defaultChoose);
		}

		public static DialogResult Show(Form parent, string title, string message, Decide defaultChoose)
		{
			return Show(parent, title, message, Timeout.Infinite, defaultChoose);
		}

		public static DialogResult Show(Form parent, string title, string message, int intervalInMilliseconds, Decide defaultChoose)
		{
			var cb = new ConfirmBox(title, message, intervalInMilliseconds, defaultChoose)
				{
					ParentWindow = parent
				};
		    if (intervalInMilliseconds == Timeout.Infinite)
		    {
		        cb.PinMessage();
		    }
		    else
		    {
		        cb.UnpinMessage();
		    }
			return Show(cb);
		}

	    private void t_Close_Tick(object sender, EventArgs e)
		{
			AcceptButton.PerformClick();
		}

	    private void ConfirmBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
			AcceptButton = defaultChoose ? btn_Yes : btn_No;
			CancelButton = btn_No;
			FocusAcceptButton();
		}
	}
}
