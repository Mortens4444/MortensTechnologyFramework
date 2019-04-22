using System;
using System.Diagnostics;

namespace Mtf.Windows.Hook
{
    public abstract class HookBase
    {
        public IntPtr GetMainModuleHandle()
        {
            using (var process = Process.GetCurrentProcess())
            {
                using (var module = process.MainModule)
                {
                    return WinApi.GetModuleHandle(module.ModuleName);
                }
            }
        }
    }
}