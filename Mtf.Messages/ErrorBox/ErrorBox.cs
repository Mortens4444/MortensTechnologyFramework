using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Mtf.Log;
using Mtf.Reflection.ExceptionInfo;

namespace Mtf.Messages.ErrorBox
{
	public sealed partial class ErrorBox : BaseBox.BaseBox
	{
	    private ErrorBox(string title, string message, int intervalInMilliseconds)
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

			tt_Hint_2.SetToolTip(btn_SendToClipboard, CopyToClipboard);
			tt_Hint_3.SetToolTip(btn_SendMessage, SendErrorReport);
		    TopMost = true;
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

	    private static DialogResult Show(ErrorBox eb)
		{
			if (eb.ParentWindow != null)
			{
				eb.Left = eb.ParentWindow.Left + (eb.ParentWindow.Width - eb.Width) / 2;
				eb.Top = eb.ParentWindow.Top + (eb.ParentWindow.Height - eb.Height) / 2;
			}
			else eb.StartPosition = FormStartPosition.CenterScreen;

			Console.Beep(440, 200);
		    try
		    {
		        return eb.ShowDialog();
		    }
		    catch
		    {
		        return DialogResult.None;
		    }
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

		public static void ShowLastWin32Error()
		{
			Show(new Win32Exception(Marshal.GetLastWin32Error()));
		}
        
		public static void ShowFileNotFound(string filename)
		{
			Show(Constants.GENERAL_ERROR, String.Concat(Constants.FILE_NOT_FOUND, filename));
		}
		
		public static void ShowLastWin32ErrorIfNotSuccess()
		{
			var errorCode = Marshal.GetLastWin32Error();
		    if (errorCode != 0)
		    {
		        Show(new Win32Exception(errorCode));
		    }
		}

		public static DialogResult Show(Exception ex)
		{
			var ed = new ExceptionDetails(ex);
			return Show(null, ed.ExceptionType, ed.Details, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult ShowServiceNotification(Exception ex)
		{
			return MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
		}

		public static DialogResult Show(Exception ex, int intervalInMilliseconds)
		{
			var ed = new ExceptionDetails(ex);
			return Show(null, ed.ExceptionType, ed.Details, intervalInMilliseconds);
		}

		public static DialogResult Show(Form parent, Exception ex)
		{
			var ed = new ExceptionDetails(ex);
			return Show(parent, ed.ExceptionType, ed.Details, Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(Form parent, string title, Exception ex)
		{
			return Show(parent, title, ex.GetDetails(), Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(string title, Exception ex)
		{
			return Show(null, title, ex.GetDetails(), Constants.MILLISECONDS_LEFT);
		}

		public static DialogResult Show(string title, Exception ex, int intervalInMilliseconds)
		{
			return Show(null, title, ex.GetDetails(), intervalInMilliseconds);
		}

		public static DialogResult Show(Form parent, string title, string message, int intervalInMilliseconds)
		{
			var eb = new ErrorBox(title, message, intervalInMilliseconds)
				{
					ParentWindow = parent
				};
		    if (intervalInMilliseconds != Timeout.Infinite)
		    {
		        eb.UnpinMessage();
		    }
			return Show(eb);
		}

	    private void t_Close_Tick(object sender, EventArgs e)
		{
			Close();
		}

	    private void ErrorBox_Shown(object sender, EventArgs e)
		{
			rtb_Message.Select(0, 0);
			SaveMessage();
		}

	    private void tsmi_Copy_Click(object sender, EventArgs e)
		{
			ToClipboard();
		}

	    private void SaveMessage()
	    {
	        var now = DateTime.UtcNow;
			var errorDetails = new StringBuilder();
			errorDetails.AppendLine($"{now.ToShortDateString()} {now.ToLongTimeString()}");
			errorDetails.AppendLine(rtb_Message.Text);
			errorDetails.AppendLine();
			errorDetails.AppendLine(new StackTrace().ToString());

	        var folder = String.Concat(Application.ProductName, " - ", Application.ProductVersion);
	        var savedErrorMessagesFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\{folder}\\";

	        var fileLogger = new FileLogger(savedErrorMessagesFolder, "saved_error_messages.log");
	        fileLogger.Log(errorDetails.ToString());
		}

	    private void btn_SendMessage_Click(object sender, EventArgs e)
		{
			PinMessage();
		    // FIXME
		    // TODO: Add Error Report
			//var ser = new SendErrorReport(String.Concat(Text, ": ", rtb_Message.Text, Environment.NewLine, Environment.NewLine, new StackTrace().ToString()));
			//ser.Show();
		}

	    private void btn_SendToClipboard_Click(object sender, EventArgs e)
		{
			ToClipboard();
		}

	    private void ToClipboard()
		{
			try
			{
				rtb_Message.SelectAll();
				rtb_Message.Focus();
				SendKeys.Send("^(C)");
			}
			catch (Exception ex)
			{
				Show(ex);
			}
		}
	}
}
