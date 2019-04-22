/*using System;
using Microsoft.Win32;
using System.Management;
using System.ComponentModel;

namespace Mtf.Windows.Registry
{
    public static class RegistryUtils
    {
        public static void CreateRegistryKey(RegistryRootKey root, string key)
        {
            switch (root)
            {
                case RegistryRootKey.HKEY_CLASSES_ROOT:
                    Registry.ClassesRoot.CreateSubKey(key);
                    break;
                case RegistryRootKey.HKEY_CURRENT_CONFIG:
                    Registry.CurrentConfig.CreateSubKey(key);
                    break;
                case RegistryRootKey.HKEY_CURRENT_USER:
                    Registry.CurrentUser.CreateSubKey(key);
                    break;
                case RegistryRootKey.HKEY_LOCAL_MACHINE:
                    Registry.LocalMachine.CreateSubKey(key);
                    break;
                case RegistryRootKey.HKEY_USERS:
                    Registry.Users.CreateSubKey(key);
                    break;
            }
        }

        /// <summary>
        /// byte[] order = (byte[])RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SOFTWARE\\Graffiti\\NonStop Video Recorder\\Preview", "Order", RegistryValueKind.Binary);
        /// uint number_of_users = (uint)RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SOFTWARE\\Graffiti\\Video Server\\Users", "NumUsers", RegistryValueKind.DWord);
        /// string expanded_string = (string)RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SYSTEM\\CurrentControlSet\\Control", "ServiceControlManagerExtension", RegistryValueKind.ExpandString);
        /// string[] multi_string = (string[])RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SYSTEM\\Setup", "CloneTag", RegistryValueKind.MultiString);
        /// string[] multi_string = (string[])RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SYSTEM\\Setup", "CloneTag", RegistryValueKind.QWord);
        /// string s = (string)RegistryUtils.ReadRemoteRegistry(cb_IPOrHostname.Text, "IPVS37", "72svpi", RegHive.LocalMachine, "SOFTWARE\\Graffiti\\Video Server\\Users", "Login Name-0", RegistryValueKind.String);
        /// </summary>
        /// <param name="ipAddress">The IP address of the remote computer.</param>
        /// <param name="username">The operating system username.</param>
        /// <param name="password">The operating system username's password.</param>
        /// <param name="hive">The hive of the registry key.</param>
        /// <param name="key">The registry key.</param>
        /// <param name="value">The registry value.</param>
        /// <param name="valueKind">The registry value type.</param>
        /// <returns>The read registry object.</returns>
        public static object ReadRemoteRegistry(string ipAddress, string username, string password, RegHive hive, string key, string value, RegistryValueKind valueKind)
        {
            if (!NetUtils.IsLocalIPAddress(ipAddress))
            {
                var connectionOptions = new ConnectionOptions
                {
                    Username = username,
                    Password = password
                };

                var scope = new ManagementScope($"\\\\{ipAddress}\\root\\default", connectionOptions);
                if (!scope.IsConnected)
                    scope.Connect();
                var registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);

                string methodName, valueName;

                switch (valueKind)
                {
                    case RegistryValueKind.Binary:
                        methodName = "GetBinaryValue";
                        valueName = "uValue";
                        break;
                    case RegistryValueKind.DWord:
                        methodName = "GetDWORDValue";
                        valueName = "uValue";
                        break;
                    case RegistryValueKind.ExpandString:
                        methodName = "GetExpandedStringValue";
                        valueName = "sValue";
                        break;
                    case RegistryValueKind.MultiString:
                        methodName = "GetMultiStringValue";
                        valueName = "sValue";
                        break;
                    case RegistryValueKind.QWord:
                        methodName = "GetQWORDValue";
                        valueName = "uValue";
                        break;
                    case RegistryValueKind.String:
                        methodName = "GetStringValue";
                        valueName = "sValue";
                        break;
                    case RegistryValueKind.Unknown:
                        methodName = "GetBinaryValue";
                        valueName = "uValue";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                var inputParameters = registry.GetMethodParameters(methodName);
                inputParameters["hDefKey"] = hive;
                inputParameters["sSubKeyName"] = key;
                inputParameters["sValueName"] = value;
                var outputParameters = registry.InvokeMethod(methodName, inputParameters, null);

                var result = Convert.ToUInt32(outputParameters["ReturnValue"]);
                if (result == 0) return outputParameters[valueName];
                throw new Win32Exception((int)result);
            }
            //throw new Exception("Using localhost address is not allowed here");
            using (var regkey = GetRegistryKey(RegHiveToRegistryRootKey(hive), key))
            {
                switch (valueKind)
                {
                    case RegistryValueKind.Binary:
                        return regkey.GetValue(value);
                    case RegistryValueKind.DWord:
                        return Convert.ToUInt32(regkey.GetValue(value));
                    case RegistryValueKind.ExpandString:
                        return Convert.ToString(regkey.GetValue(value));
                    case RegistryValueKind.MultiString:
                        return Convert.ToString(regkey.GetValue(value));
                    case RegistryValueKind.QWord:
                        return Convert.ToUInt32(regkey.GetValue(value));
                    case RegistryValueKind.String:
                        return Convert.ToString(regkey.GetValue(value));
                    case RegistryValueKind.Unknown:
                        return regkey.GetValue(value);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public static string[] GetSubKeysOfRemoteRegistryKey(string ipAddress, string username, string password, RegHive hive, string key)
        {
            if (!NetUtils.IsLocalIPAddress(ipAddress))
            {
                var connection_options = new ConnectionOptions
                {
                    Username = username,
                    Password = password
                };

                var scope = new ManagementScope($"\\\\{ipAddress}\\root\\default", connection_options);
                if (!scope.IsConnected)
                    scope.Connect();
                var registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);

                const string methodName = "EnumKey";

                var inputParameters = registry.GetMethodParameters(methodName);
                inputParameters["hDefKey"] = hive;
                inputParameters["sSubKeyName"] = key;
                var outputParameters = registry.InvokeMethod(methodName, inputParameters, null);

                var result = (string[])outputParameters["sNames"];
                return result ?? new string[0];

            }
            throw new Exception("Using localhost address is not allowed here");
        }

        public static RegistryValueKind GetRegistryValueKind(uint regValueType)
        {
            switch (regValueType)
            {
                case 1:
                    return RegistryValueKind.String;
                case 2:
                    return RegistryValueKind.ExpandString;
                case 3:
                    return RegistryValueKind.Binary;
                case 4:
                    return RegistryValueKind.DWord;
                case 7:
                    return RegistryValueKind.MultiString;
                default:
                    return RegistryValueKind.Unknown;
            }
        }

        public static string[] GetSubValuesAndTypesOfRemoteRegistryKey(string ipAddress, string username, string password, RegHive hive, string key)
        {
            string[] result = null;
            if (NetUtils.IsLocalIPAddress(ipAddress))
                throw new Exception("Using localhost address is not allowed here");

            var connectionOptions = new ConnectionOptions
            {
                Username = username,
                Password = password
            };

            var scope = new ManagementScope($"\\\\{ipAddress}\\root\\default", connectionOptions);
            if (!scope.IsConnected)
                scope.Connect();
            var registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);

            const string methodName = "EnumValues";

            var inputParameters = registry.GetMethodParameters(methodName);
            inputParameters["hDefKey"] = hive;
            inputParameters["sSubKeyName"] = key;
            var outputParameters = registry.InvokeMethod(methodName, inputParameters, null);

            var names = (string[])outputParameters["sNames"];
            if (names != null)
            {
                var types = (uint[])outputParameters["Types"];
                result = new string[names.Length];
                for (var i = 0; i < result.Length; i++)
                    result[i] = $"{names[i]}\\{types[i]}";
            }

            return result ?? new string[0];
        }

        public static RegistryRootKey RegHiveToRegistryRootKey(RegHive hive)
        {
            switch (hive)
            {
                case RegHive.ClassesRoot:
                    return RegistryRootKey.HKEY_CLASSES_ROOT;
                case RegHive.CurrentUser:
                    return RegistryRootKey.HKEY_CURRENT_USER;
                case RegHive.LocalMachine:
                    return RegistryRootKey.HKEY_LOCAL_MACHINE;
                case RegHive.Users:
                    return RegistryRootKey.HKEY_USERS;
                case RegHive.CurrentConfig:
                    return RegistryRootKey.HKEY_CURRENT_CONFIG;
                default:
                    throw new NotImplementedException();
            }
        }

        public static RegistryHive RegHiveToRegistryHive(RegHive hive)
        {
            switch (hive)
            {
                case RegHive.ClassesRoot:
                    return RegistryHive.ClassesRoot;
                case RegHive.CurrentUser:
                    return RegistryHive.CurrentUser;
                case RegHive.LocalMachine:
                    return RegistryHive.LocalMachine;
                case RegHive.Users:
                    return RegistryHive.Users;
                case RegHive.CurrentConfig:
                    return RegistryHive.CurrentConfig;
                case RegHive.DynData:
                    return RegistryHive.DynData;
                default: throw new NotImplementedException();
            }
        }

        public static RegHive RegistryHiveToRegHive(RegistryHive hive)
        {
            switch (hive)
            {
                case RegistryHive.ClassesRoot:
                    return RegHive.ClassesRoot;
                case RegistryHive.CurrentConfig:
                    return RegHive.CurrentConfig;
                case RegistryHive.CurrentUser:
                    return RegHive.CurrentUser;
                case RegistryHive.DynData:
                    return RegHive.DynData;
                case RegistryHive.LocalMachine:
                    return RegHive.LocalMachine;
                //case RegistryHive.PerformanceData:
                //	break;
                case RegistryHive.Users:
                    return RegHive.Users;
                default: throw new NotImplementedException();
            }
        }

        public static RegistryKey GetRegistryKey(RegistryRootKey rootkey, string registryKey)
        {
            return GetRegistryKey(rootkey, registryKey, false);
        }

        public static RegistryKey GetRegistryKey(RegistryRootKey rootkey, string registryKey, bool forWriting)
        {
            RegistryKey key;

            switch (rootkey)
            {
                case RegistryRootKey.HKEY_CLASSES_ROOT:
                    key = Registry.ClassesRoot;
                    break;
                case RegistryRootKey.HKEY_CURRENT_CONFIG:
                    key = Registry.CurrentConfig;
                    break;
                case RegistryRootKey.HKEY_CURRENT_USER:
                    key = Registry.CurrentUser;
                    break;
                case RegistryRootKey.HKEY_LOCAL_MACHINE:
                    key = Registry.LocalMachine;
                    break;
                default: //case RegistryRootKey.HKEY_USERS:
                    key = Registry.Users;
                    break;
            }

            return key.OpenSubKey(registryKey, forWriting);
        }

        public static void ExportRegistry(string fileName)
        {
            Utils.RunProgramOrFile("regedit.exe", String.Concat("/E \"", fileName, "\""), false, true);
        }

        public static void CreateValue(RegistryRootKey rootkey, string registryKey, string valueName, object value)
        {
            using (var key = GetRegistryKey(rootkey, registryKey, true))
            {
                key.SetValue(valueName, value);
            }
        }

        public static RegistryKey GetSubKey(RegistryRootKey rootkey, string registryKey, string subkeyName)
        {
            if (registryKey == null) throw new ArgumentNullException(nameof(registryKey));
            using (var key = GetRegistryKey(rootkey, registryKey))
            {
                return key.OpenSubKey(subkeyName);
            }
        }

        public static RegistryKey GetSubKey(RegistryKey key, string subkeyName, bool writeable)
        {
            return key.OpenSubKey(subkeyName, writeable);
        }

        public static RegistryKey GetSubKey(RegistryKey key, string subkeyName)
        {
            return key.OpenSubKey(subkeyName);
        }

        public static RegistryInfo SearchForRegistryValue(RegistryRootKey rootkey, string searchInRegistryKey, string value)
        {
            using (var key = GetRegistryKey(rootkey, searchInRegistryKey))
            {
                return SearchForRegistryValue(key, value);
            }
        }

        public static RegistryInfo SearchForRegistryValue(RegistryRootKey rootkey, string searchInRegistryKey, string valueName, string value)
        {
            using (var key = GetRegistryKey(rootkey, searchInRegistryKey))
            {
                return SearchForRegistryValue(key, valueName, value);
            }
        }

        public static RegistryInfo SearchForRegistryValue(RegistryKey searchInRegistryKey, string value)
        {
            if (searchInRegistryKey == null) return null;

            var values = searchInRegistryKey.GetValueNames();
            foreach (var actualValue in values)
            {
                var av = searchInRegistryKey.GetValue(actualValue);
                if (av.ToString().IndexOf(value) <= 0)
                    continue;

                var ri = new RegistryInfo(searchInRegistryKey, actualValue, av);
                return ri;
            }

            var subkeys = searchInRegistryKey.GetSubKeyNames();
            foreach (var subkeyName in subkeys)
            {
                using (var subkey = searchInRegistryKey.OpenSubKey(subkeyName))
                {
                    var result = SearchForRegistryValue(subkey, value);
                    if (result != null) return result;
                }
            }
            return null;
        }

        public static RegistryInfo SearchForRegistryValue(RegistryKey searchInRegistryKey, string valueName, string value)
        {
            if (searchInRegistryKey == null) return null;

            var av = searchInRegistryKey.GetValue(valueName);
            if (av?.ToString().IndexOf(value) > 0)
            {
                var ri = new RegistryInfo(searchInRegistryKey, valueName, av);
                return ri;
            }

            var subkeys = searchInRegistryKey.GetSubKeyNames();
            foreach (var subkey_name in subkeys)
            {
                using (var subkey = searchInRegistryKey.OpenSubKey(subkey_name))
                {
                    var result = SearchForRegistryValue(subkey, valueName, value);
                    if (result != null) return result;
                }
            }
            return null;
        }
    }
}*/