using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Mtf.Messages.ConfirmBox;

namespace Mtf.Messages.ThreeOptionConfirmBox
{
	public partial class ThreeOptionConfirmBox : BaseBox.BaseBox
	{
	    private bool? defaultChoose;

	    private ThreeOptionConfirmBox(string title, string message, int interval, Decide? defaultChoose)
		{
			InitializeComponent();
			btn_Yes.Text = Yes;
			btn_No.Text = No;
			btn_Cancel.Text = Cancel;
			Text = title;
			rtb_Message.Text = message;
			t_Close.Enabled = false;

			if (defaultChoose == null)
			{
				this.defaultChoose = null;
			}
			else
			{
				this.defaultChoose = Decide.Yes == defaultChoose;
				AcceptButton = this.defaultChoose.Value ? btn_Yes : btn_No;
				CancelButton = btn_No;
			}
			if (interval != Timeout.Infinite) t_Close.Interval = interval;
			else
			{
				btn_Pin.Visible = false;
				btn_Unpin.Visible = false;
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
			if (defaultChoose == null) btn_Cancel.Focus();
			else if (defaultChoose.Value) btn_Yes.Focus();
			else btn_No.Focus();
		}

	    private void PinMessage()
		{
			t_Close.Stop();
			t_DecrementSecondsLeft.Stop();
			btn_Pin.Visible = false;
			btn_Unpin.Visible = true;
			tt_Hint.SetToolTip(btn_Unpin, EnableAutomaticMessageClosing);
			btn_Yes.Text = Yes;
			btn_No.Text = No;
			btn_Cancel.Text = Cancel;
			FocusAcceptButton();
		}

	    private void UnpinMessage()
		{
			btn_Pin.Visible = true;
			btn_Unpin.Visible = false;
			tt_Hint.SetToolTip(btn_Pin, DisableAutomaticMessageClosing);
			t_Close.Start();
			t_DecrementSecondsLeft.Start();
			SecondsLeft = (int)(Math.Truncate((decimal)t_Close.Interval / 1000));
			ShowMessageOnDefaultButton();
		}

	    private void ShowMessageOnDefaultButton()
		{
			var okSecondsLeft = new StringBuilder();
		    if (defaultChoose == null)
		    {
		        okSecondsLeft.Append(Cancel);
		    }
		    else if (defaultChoose.Value)
		    {
		        okSecondsLeft.Append(Yes);
		    }
		    else
		    {
		        okSecondsLeft.Append(No);
		    }
		    okSecondsLeft.AppendFormat(" ({0})", SecondsLeft);
		    if (defaultChoose == null)
		    {
		        btn_Cancel.Text = okSecondsLeft.ToString();
		    }
			else if (defaultChoose.Value)
		    {
		        btn_Yes.Text = okSecondsLeft.ToString();
		    }
		    else
		    {
		        btn_No.Text = okSecondsLeft.ToString();
		    }
		}

		private static DialogResult Show(ThreeOptionConfirmBox cb)
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

		public static DialogResult Show(string title, string message, int interval, Decide defaultChoose)
		{
			return Show(null, title, message, interval, defaultChoose);
		}

		public static DialogResult Show(string title, string message, Decide defaultChoose)
		{
			return Show(null, title, message, Timeout.Infinite, defaultChoose);
		}

		public static DialogResult Show(Form parent, string title, string message, Decide defaultChoose)
		{
			return Show(parent, title, message, Timeout.Infinite, defaultChoose);
		}

		public static DialogResult Show(Form parent, string title, string message, int interval, Decide defaultChoose)
		{
			var cb = new ThreeOptionConfirmBox(title, message, interval, defaultChoose)
				{
					ParentWindow = parent
				};
		    if (interval == Timeout.Infinite)
		    {
		        cb.PinMessage();
		    }
		    else
		    {
		        cb.UnpinMessage();
		    }
			return Show(cb);
		}

	    private void ThreeOptionConfirmBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
		}

	    private void t_Close_Tick(object sender, EventArgs e)
		{
			AcceptButton.PerformClick();
		}

	    private void btn_Pin_Click(object sender, EventArgs e)
		{
			PinMessage();
		}

	    private void btn_Unpin_Click(object sender, EventArgs e)
		{
			UnpinMessage();
		}

	    private void t_DecrementSecondsLeft_Tick(object sender, EventArgs e)
		{
			SecondsLeft--;
			ShowMessageOnDefaultButton();
		}
	}
}
