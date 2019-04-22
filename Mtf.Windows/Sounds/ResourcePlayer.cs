using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mtf.Windows.Enum;

namespace Mtf.Windows.Sounds
{
    public class ResourcePlayer
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("Winmm.dll", SetLastError = true)]
        public static extern bool PlaySound(string pszSound, IntPtr hmod, SoundFlags fdwSound);

        [DllImport("Winmm.dll", SetLastError = true)]
        public static extern bool PlaySound(byte[] pszSound, IntPtr hmod, SoundFlags fdwSound);

        private readonly string resourceName;
        private readonly bool loop;
        private readonly byte repeations;

        public ResourcePlayer(string resourceName, bool loop = false, byte repeations = 1)
        {
            this.resourceName = resourceName;
            this.loop = loop;
            this.repeations = repeations;
        }

        public void Play()
        {
            Task.Factory.StartNew(() =>
            {
                if (loop)
                {
                    PlaySound(resourceName, GetModuleHandle(null), SoundFlags.SND_RESOURCE | SoundFlags.SND_SYNC | SoundFlags.SND_NODEFAULT | SoundFlags.SND_LOOP);
                }
                else
                {
                    for (byte i = 0; i < repeations; i++)
                    {
                        PlaySound(resourceName, GetModuleHandle(null), SoundFlags.SND_RESOURCE | SoundFlags.SND_SYNC | SoundFlags.SND_NODEFAULT);
                    }
                }
            });
        }
    }
}