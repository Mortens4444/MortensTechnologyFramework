using Mtf.Core.Network.Sockets;
using Mtf.Network.Sockets;
using Ninject.Modules;

namespace Mtf.Network
{
    public class NetworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISocketCloser>().To<SocketCloser>();
        }
    }
}