using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace Mtf.Messages.WaitForm
{
	public partial class WaitForm : Form
	{
	    public delegate void VoidResultVoidParams();

	    private bool close;

		/// <summary>
		/// Call this method on an another thread then the worker thead or it won't be visible
		/// </summary>
		public WaitForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Call this method on an another thread then the worker thead or it won't be visible
		/// </summary>
		/// <param name="waitMessage">The message to show. Default message is: Please wait... This operation could take some time</param>
		public WaitForm(string waitMessage)
		{
			InitializeComponent();
			l_WaitMessage.Text = waitMessage;
		}

		/// <summary>
		/// Call this method to show WaitForm
		/// </summary>
		public new void ShowDialog()
		{
			Show();
		}

		/// <summary>
		/// After work is comleted call this method
		/// </summary>
		public void CloseIt()
		{
		    if (!InvokeRequired)
		    {
		        close = true;
		        Close();
		    }
		    else
		    {
		        Invoke(new VoidResultVoidParams(CloseIt));
		    }
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = !close;
			base.OnClosing(e);
		}

		/// <summary>
		/// The message to show.
		/// </summary>
		public string WaitMessage
		{
		    get
		    {
		        return Invocations.GetControlText(l_WaitMessage);
		    }
		    set
		    {
		        Invocations.SetControlText(l_WaitMessage, value);
		    }
		}

		private void WaitForm_Deactivate(object sender, EventArgs e)
		{
			Focus();
		}

		private void WaitForm_Shown(object sender, EventArgs e)
		{
			bw_Refresh.RunWorkerAsync();
		}

		private void bw_Refresh_DoWork(object sender, DoWorkEventArgs e)
		{
			while (!close)
			{
				Invalidate();
				Thread.Sleep(200);
			}
		}
	}
}
