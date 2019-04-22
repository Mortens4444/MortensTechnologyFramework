using System.IO;
using System.Media;

namespace Mtf.Sounds.Player
{
    public class FilePlayer : SoundPlayerBase
    {
        public string Filename { get; private set; }

        public FilePlayer(string filename, byte repeations)
            : base(repeations)
        {
            Initialize(filename);
        }

        public FilePlayer(string filename, bool loop)
            : base(loop: loop)
        {
            Initialize(filename);
        }

        private void Initialize(string filename)
        {
            CheckFileExistance(filename);
            Filename = filename;
        }

        public override void Play()
        {
            var soundPlayer = new SoundPlayer(Filename);
            soundPlayer.LoadAsync();
            soundPlayer.LoadCompleted += (sender, args) =>
            {
                soundPlayer.Play(Loop, Repeations);
            };
        }

        private static void CheckFileExistance(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File not found: {filename}", filename);
            }
        }
    }
}