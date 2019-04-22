using System;
using System.Linq;

namespace Mtf.Utils.ByteArrayExtensions
{
    public static class BaseExtensions
    {
        private const int NotFound = -1;

        public static int Find(this byte[] array, byte[] subArray)
        {
            return array.Find(subArray, 0);
        }

        public static void Replace(this byte[] array, byte oldValue, byte newValue)
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] == oldValue)
                {
                    array[i] = newValue;
                }
            }
        }

        public static int Find(this byte[] array, byte[] subArray, int startIndex)
        {
            if (subArray == null || subArray.Length == 0 || subArray.Length > array.Length)
            {
                return NotFound;
            }

            var index = startIndex - 1;

            while (index < array.Length)
            {
                index = Array.IndexOf(array, subArray[0], index + 1);
                if (index == NotFound)
                {
                    return NotFound;
                }

                var notFound = false;
                var i = 1;
                while (i < subArray.Length)
                {
                    index++;
                    if (index >= array.Length)
                    {
                        return NotFound;
                    }
                    if (array[index] == subArray[i++])
                    {
                        continue;
                    }
                    notFound = true;
                    break;
                }
                if (notFound)
                {
                    continue;
                }

                return index - subArray.Length + 1;
            }
            return NotFound;
        }

        public static int Find(this byte[] array, byte[] subArray, int startIndex, int count)
        {
            while (count >= subArray.Length)
            {
                if (startIndex > NotFound)
                {
                    var index = Array.IndexOf(array, subArray[0], startIndex, count - subArray.Length + 1);
                    if (index == NotFound)
                        return NotFound;

                    int i, p;
                    for (i = 0, p = index; i < subArray.Length; i++, p++)
                        if (array[p] != subArray[i])
                            break;

                    if (i == subArray.Length)
                    {
                        return index;
                    }

                    count -= index - startIndex + 1;
                    startIndex = index + 1;
                }
                else
                {
                    startIndex = 0; // FIX of ArgumentOutOfRangeException
                }
            }
            return NotFound;
        }

        public static byte[] AppendArrays(this byte[] value, params byte[][] arrays)
        {
            var length = value.Length + arrays.Sum(t => t.Length);
            var result = new byte[length];
            Array.Copy(value, 0, result, 0, value.Length);
            length = value.Length;
            foreach (var byteArray in arrays)
            {
                Array.Copy(byteArray, 0, result, length, byteArray.Length);
                length += byteArray.Length;
            }
            return result;
        }

        public static byte[] SubArray(this byte[] value, int index, int length)
        {
            return value.CreateArray(index, length);
        }

        public static byte[] CreateArray(this byte[] value, int index, int length)
        {
            var result = new byte[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = value[i + index];
            }
            return result;
        }
    }
}