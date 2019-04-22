using Microsoft.Win32;

namespace Mtf.Windows.Registry
{
    public class RegistryInfo
    {
        public RegistryKey Key { get; }
        public string ValueName { get; }
        public object Value { get; }

        public RegistryInfo(RegistryKey key, string valueName, object value)
        {
            Key = key;
            ValueName = valueName;
            Value = value;
        }

        ~RegistryInfo()
        {
            Key?.Close();
        }
    }
}