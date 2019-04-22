using Mtf.File.Common;

namespace Mtf.File.Read
{
    public class ConfigReader : ConfigBase
    {
        public ConfigReader(string configFile) : base(configFile)
        { }

        public string Get(string settingName)
        {
            var setting = config.AppSettings.Settings[settingName];
            return setting.Value;
        }

        public string this[string settingName] => Get(settingName);
    }
}