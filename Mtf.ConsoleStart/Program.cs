using System;
using Mtf.Core;
using Mtf.Core.Cryptography;

namespace Mtf.ConsoleStart
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var initializer = new NinjectInitializer();
            initializer.Start();

            var authentication = initializer.Get<IAuthentication>();
            var loginText = authentication.LOGIN("username", "password");
            Console.WriteLine(loginText);
        }
    }
}