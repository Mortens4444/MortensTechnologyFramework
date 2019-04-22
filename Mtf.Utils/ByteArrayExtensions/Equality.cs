using Mtf.Utils.DoubleExtensions;
using Mtf.Utils.Types;

namespace Mtf.Utils.ByteArrayExtensions
{
    public static class Equality
    {
        public static Percent EqualsPercent(this byte[] array1, byte[] array2)
        {
            return EqualInPercent(array1, array2);
        }

        public static Percent EqualInPercent(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null)
            {
                return 0;
            }
            if (array1.Length == 0 && array2.Length == 0)
            {
                return 100;
            }

            int minLength, maxLength;
            if (array1.Length <= array2.Length)
            {
                minLength = array1.Length;
                maxLength = array2.Length;
            }
            else
            {
                maxLength = array1.Length;
                minLength = array2.Length;
            }

            var same = 0;
            for (var i = 0; i < minLength; i++)
            {
                if (array1[i] == array2[i])
                {
                    same++;
                }
            }

            return ((double)same / maxLength * 100).LimitMeWithRound(0, 100);
        }

        public static bool IsEqual(byte[] array1, byte[] array2)
        {
            if (array1 == null && array2 == null)
            {
                return true;
            }
            if (array1 == null || array2 == null)
            {
                return false;
            }
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}