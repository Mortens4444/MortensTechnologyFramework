using System;

namespace Mtf.Network
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class BitfieldLengthAttribute : Attribute
    {
        public BitfieldLengthAttribute(uint length)
        {
            Length = length;
        }

        public uint Length { get; }
    }
}