namespace Mtf.Sounds.Player
{
    public abstract class SoundPlayerBase
    {
        public bool Loop { get; protected set;  }

        public byte Repeations { get; protected set; }

        protected SoundPlayerBase(byte repeations = 0, bool loop = false)
        {
            Repeations = repeations;
            Loop = loop;
        }

        public abstract void Play();
    }
}