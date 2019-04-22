using System.Runtime.InteropServices;
using Mtf.Utils.DateExtensions;

namespace Mtf.Windows.DateTime
{
    public class DataTimeChanger
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME lpSystemTime);

        public void SetDateTime(SYSTEMTIME systemTime)
        {
            SetSystemTime(ref systemTime);
        }
    }
}