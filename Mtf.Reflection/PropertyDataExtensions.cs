using System;
using System.Management;

namespace Mtf.Reflection
{
    public static class PropertyDataExtensions
    {
        public static string GetValue(PropertyData propertyData)
        {
            var propertyDataType = propertyData.Value.GetType().ToString();
            if (propertyDataType.EndsWith("[]"))
            {
                var arrayValues = propertyData.Value as Array;
                return String.Join("\t", arrayValues);
            }

            /*switch ()
            {
                case "System.String[]":
                    var strArray = (string[])propertyData.Value;
                    return String.Join("\t", strArray);
                case "System.UInt16[]":
                    var shortArray = (ushort[])propertyData.Value;
                    return String.Join("\t", shortArray);
            }*/
            return propertyData.Value.ToString();
        }

        private static T CastToType<T>(object value)
        {
            return (T)value;
        }
    }
}