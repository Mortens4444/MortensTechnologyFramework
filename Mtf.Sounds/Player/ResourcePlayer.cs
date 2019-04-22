using System;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;

namespace Mtf.Sounds.Player
{
    public class ResourcePlayer : SoundPlayerBase
    {
        private readonly Assembly assembly;
        private readonly string resourceName;

        public ResourcePlayer(Assembly assembly, string resourceName, byte repeations)
            : base(repeations)
        {
            this.assembly = assembly;
            this.resourceName = resourceName;
        }

        public override void Play()
        {
            Task.Factory.StartNew(() =>
            {
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new ArgumentNullException(nameof(stream));
                    }
                    var soundPlayer = new SoundPlayer(stream);
                    soundPlayer.Play(Loop, Repeations);
                    stream.Close();
                }
            });
        }
    }
}