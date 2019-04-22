namespace Mtf.Utils.Generics
{
    public class EqualityChecker<TType>
    {
        public static bool operator ==(TType dts1, TType dts2)
        {
            return Equality.IsEqual(dts1, dts2);
        }

        public static bool operator !=(TType dts1, TType dts2)
        {
            return Equality.IsNotEqual(dts1, dts2);
        }
    }
}