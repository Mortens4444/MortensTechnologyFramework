using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Mtf.Messages.InfoBox
{
	public partial class InfoBox : BaseBox.BaseBox
	{
		protected InfoBox() { }

	    private InfoBox(string title, string message, int intervalInMilliseconds)
		{
			InitializeComponent();
			btn_Ok.Text = Ok;
			Text = String.Concat(Application.ProductName, ": ", title);
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
			var okSecondsLeft = new StringBuilder(Ok);
			okSecondsLeft.AppendFormat(" ({0})", SecondsLeft);
			btn_Ok.Text = okSecondsLeft.ToString();
		}

	    private void t_DecrementSecondsLeft_Tick(object sender, EventArgs e)
		{
			SecondsLeft--;
			ShowMessageOnOkButton();
		}

	    private static DialogResult Show(InfoBox ib)
		{
			if (ib.ParentWindow != null)
			{
				ib.Left = ib.ParentWindow.Left + (ib.ParentWindow.Width - ib.Width) / 2;
				ib.Top = ib.ParentWindow.Top + (ib.ParentWindow.Height - ib.Height) / 2;
			}
			else ib.StartPosition = FormStartPosition.CenterScreen;
			return ib.ShowDialog();
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
			var ib = new InfoBox(title, message, intervalInMilliseconds)
				{
					ParentWindow = parent
				};
		    if (intervalInMilliseconds != Timeout.Infinite)
		    {
		        ib.UnpinMessage();
		    }
			return Show(ib);
		}

	    private void t_Close_Tick(object sender, EventArgs e)
		{
			Close();
		}

	    private void InfoBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
		}
	}
}
