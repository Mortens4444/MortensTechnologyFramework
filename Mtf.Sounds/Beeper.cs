using System;
using Mtf.Sounds.Enum;

namespace Mtf.Sounds
{
    public class Beeper
    {
        public void PlaySound(Sound sound, int milliseconds)
        {
            PlaySound((ushort)sound, milliseconds);
        }

        public void PlaySound(ushort sound, int milliseconds)
        {
            Console.Beep(sound, milliseconds);
        }
    }
}