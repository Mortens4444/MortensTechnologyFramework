using System;
using System.Linq;

namespace Mtf.Utils.Generics
{
    public static class Equality
    {
        public static bool IsEqualOneOfThis<T>(T obj, params T[] values)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (values == null || values.Length == 0)
            {
                return false;
            }
            return values.Contains(obj);
        }

        public static bool IsEqual<T>(T o1, T o2)
        {
            if (o1 == null && o2 == null)
            {
                return true;
            }

            if (o1 == null || o2 == null)
            {
                return false;
            }
            return o1.Equals(o2);
        }

        public static bool IsNotEqual<T>(T o1, T o2)
        {
            if (o1 == null && o2 == null)
            {
                return false;
            }

            if (o1 == null || o2 == null)
            {
                return true;
            }
            return !o1.Equals(o2);
        }
    }
}