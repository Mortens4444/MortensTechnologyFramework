using System.Configuration;

namespace Mtf.File.Common
{
    public class ConfigBase
    {
        protected readonly Configuration config;

        public ConfigBase(string configFile)
        {
            var fm = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFile
            };
            config = ConfigurationManager.OpenMappedExeConfiguration(fm, ConfigurationUserLevel.None);
        }
    }
}