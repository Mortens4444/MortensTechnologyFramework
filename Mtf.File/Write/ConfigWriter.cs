using System.Configuration;
using Mtf.File.Common;

namespace Mtf.File.Write
{
    public class ConfigWriter : ConfigBase
    {
        public ConfigWriter(string configFile) : base(configFile)
        { }

        public void Set(string settingName, object settingValue)
        {
            var setting = config.AppSettings.Settings[settingName];
            setting.Value = settingValue.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }

        public string this[string settingName]
        {
            set
            {
                Set(settingName, value);
            }
        }
    }
}