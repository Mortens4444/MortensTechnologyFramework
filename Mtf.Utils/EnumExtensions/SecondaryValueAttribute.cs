using System;

namespace Mtf.Utils.EnumExtensions
{
    public class SecondaryValueAttribute : Attribute
    {
        public int SecondaryValue { get; }

        public SecondaryValueAttribute(int secondaryValue)
        {
            SecondaryValue = secondaryValue;
        }
    }
}