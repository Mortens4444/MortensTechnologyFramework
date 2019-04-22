using System.Windows.Forms;

namespace Mtf.Messages
{
    public static class Invocations
    {
        public delegate string StringResultControlParams(Control control);

        public delegate void VoidResultControlStringParams(Control control, string str);

        public static string GetControlText(Control control)
        {
            try
            {
                if (control == null)
                {
                    return null;
                }
                if (!control.InvokeRequired)
                {
                    return control.Text;
                }
                return (string)control.Invoke(new StringResultControlParams(GetControlText), control);
            }
            catch
            {
                return null;
            }
        }

        public static void SetControlText(Control control, string text)
        {
            try
            {
                if (control == null)
                {
                    return;
                }

                if (!control.InvokeRequired)
                {
                    control.Text = text;
                    control.Refresh();
                }
                else
                {
                    control.Invoke(new VoidResultControlStringParams(SetControlText), control, text);
                }
            }
            catch { }
        }
    }
}