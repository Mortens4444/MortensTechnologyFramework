using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Mtf.Messages.WarningBox
{
	public partial class WarningBox : BaseBox.BaseBox
	{
	    private WarningBox(string title, string message, int intervalInMilliseconds)
		{
			InitializeComponent();
			btn_Ok.Text = Ok;
			Text = String.IsNullOrEmpty(title) ? Application.ProductName : String.Concat(Application.ProductName, ": ", title);
			rtb_Message.Text = message;
			t_Close.Enabled = false;
			if (intervalInMilliseconds != Timeout.Infinite)
			{
			    t_Close.Interval = intervalInMilliseconds;
			}
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

	    private static DialogResult Show(WarningBox wb)
		{
			if (wb.ParentWindow != null)
			{
				wb.Left = wb.ParentWindow.Left + (wb.ParentWindow.Width - wb.Width) / 2;
				wb.Top = wb.ParentWindow.Top + (wb.ParentWindow.Height - wb.Height) / 2;
			}
			else wb.StartPosition = FormStartPosition.CenterScreen;
			return wb.ShowDialog();
		}

		public static DialogResult Show(string title, string message, int intervalInMilliseconds)
		{
			return Show(null, title, message, intervalInMilliseconds);
		}

		public static DialogResult Show(string title, string message)
		{
			return Show(null, title, message, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(Form parent, string title, string message)
		{
			return Show(parent, title, message, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(Form parent, string title, string message, int intervalInMilliseconds)
		{
			var wb = new WarningBox(title, message, intervalInMilliseconds)
				{
					ParentWindow = parent
				};
		    if (intervalInMilliseconds != Timeout.Infinite)
		    {
		        wb.UnpinMessage();
		    }
			return Show(wb);
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
			btn_Unpin.Visible = true;
			tt_Hint.SetToolTip(btn_Unpin, EnableAutomaticMessageClosing);
			btn_Ok.Text = Ok;
		}

	    private void UnpinMessage()
		{
			btn_Pin.Visible = true;
			btn_Unpin.Visible = false;
			tt_Hint.SetToolTip(btn_Pin, DisableAutomaticMessageClosing);
			t_Close.Start();
			t_DecrementSecondsLeft.Start();
			SecondsLeft = (int)(Math.Truncate((decimal)t_Close.Interval / 1000));
			ShowMessageOnOkButton();
		}

	    private void ShowMessageOnOkButton()
		{
			var ok_seconds_left = new StringBuilder(Ok);
			ok_seconds_left.AppendFormat(" ({0})", SecondsLeft);
			btn_Ok.Text = ok_seconds_left.ToString();
		}

	    private void t_DecrementSecondsLeft_Tick(object sender, EventArgs e)
		{
			SecondsLeft--;
			ShowMessageOnOkButton();
		}

	    private void t_Close_Tick(object sender, EventArgs e)
		{
			Close();
		}

	    private void WarningBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
		}
	}
}
