using System.Reflection;

namespace Mtf.Reflection
{
    public static class PropertyExtensions
    {
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
            property.SetValue(obj, value);
        }
    }
}