using System.Media;

namespace Mtf.Sounds
{
    public static class SoundPlayerExtensions
    {
        public static void Play(this SoundPlayer soundPlayer, bool loop, byte repeations)
        {
            if (loop)
            {
                soundPlayer.PlayLooping();
            }
            else
            {
                for (byte i = 0; i < repeations; i++)
                {
                    soundPlayer.PlaySync();
                }
            }
        }
    }
}