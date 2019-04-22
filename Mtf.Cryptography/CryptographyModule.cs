using Mtf.Core.Cryptography;
using Ninject.Modules;

namespace Mtf.Cryptography
{
    public class CryptographyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBase64>().To<Base64>();
            Bind<IAuthentication>().To<Authentication>();
        }
    }
}