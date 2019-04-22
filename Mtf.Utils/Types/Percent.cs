using System;
using Mtf.Utils.ByteExtensions;

namespace Mtf.Utils.Types
{
    public struct Percent
    {
        public byte Value { get; }

        public Percent(byte value, bool throwException = true)
        {
            if (throwException && value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Percent value cannot be over 100");
            }

            var percent = value.LimitMe(0, 100);
            Value = percent;
        }

        public static implicit operator Percent(byte value)
        {
            return new Percent(value);
        }

        public static implicit operator Percent(int value)
        {
            return new Percent((byte)value);
        }

        public static implicit operator byte(Percent myself)
        {
            return myself.Value;
        }

        public override string ToString()
        {
            return $"{Value}%";
        }

        public float ToProbabitity()
        {
            return (float)Value / 100;
        }
    }

}