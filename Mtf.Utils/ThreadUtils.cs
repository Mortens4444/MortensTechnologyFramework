using System.Threading;

namespace Mtf.Utils
{
    public class ThreadUtils
    {
        public delegate void VoidResultVoidParams();

        public static Thread StartThread(VoidResultVoidParams function, bool background = false, ApartmentState state = ApartmentState.MTA, string threadName = "")
        {
            var thread = new Thread(new ThreadStart(function))
            {
                IsBackground = background,
                Name = threadName
            };
            thread.SetApartmentState(state);
            thread.Start();
            return thread;
        }
    }
}