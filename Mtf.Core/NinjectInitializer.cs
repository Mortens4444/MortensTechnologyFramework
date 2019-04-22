using System.Linq;
using System.Reflection;
using Ninject;

namespace Mtf.Core
{
    public class NinjectInitializer
    {
        private readonly StandardKernel standardKernel;

        public NinjectInitializer()
        {
            standardKernel = new StandardKernel();
        }

        public void Start()
        {
            var assemblyNames = new[]
            {
                "Mtf.Common.dll",
                "Mtf.Controls.dll",
                "Mtf.Cryptography.dll",
                "Mtf.Database.dll",
                "Mtf.ExceptionHandler.dll",
                "Mtf.File.dll",
                "Mtf.Graphics.dll",
                "Mtf.Hardware.dll",
                "Mtf.Linux.dll",
                "Mtf.Log.dll",
                "Mtf.Mailer.dll",
                "Mtf.Messages.dll",
                "Mtf.Network.dll",
                "Mtf.Reflection.dll",
                "Mtf.Service.dll",
                "Mtf.Sounds.dll",
                "Mtf.Utils.dll",
                "Mtf.Windows.dll",
                "Mtf.Windows.Hook.dll"
            };
            var assemblies = assemblyNames.Select(Assembly.LoadFile);
            standardKernel.Load(assemblies);
        }

        public TType Get<TType>()
        {
            return standardKernel.Get<TType>();
        }
    }
}