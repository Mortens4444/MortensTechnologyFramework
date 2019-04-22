using System;
using System.ComponentModel;
using System.Linq;

namespace Mtf.Utils.EnumExtensions
{
    public static class BaseExtensions
    {
        public static string GetDescription(this object value)
        {
            var attribute = GetFirstCustomAttributeWithType<DescriptionAttribute>(value);
            return attribute != null ? attribute.Description : value.ToString();
        }

        public static int GetSecondaryValue(this object value)
        {
            var attribute = GetFirstCustomAttributeWithType<SecondaryValueAttribute>(value);
            return attribute?.SecondaryValue ?? Convert.ToInt32(value);
        }

        private static TType GetFirstCustomAttributeWithType<TType>(object value)
        {
            var name = value.ToString();
            var field = value.GetType().GetField(name);
            return (TType)field.GetCustomAttributes(typeof(TType), false).FirstOrDefault();
        }

        public static Array GetItems(this Type enumType)
        {
            return System.Enum.GetValues(enumType);
        }
    }
}