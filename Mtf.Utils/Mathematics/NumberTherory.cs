using System;
using System.Collections.Generic;

namespace Mtf.Utils.Mathematics
{
    public static class NumberTherory
    {
        public static FactorizationResult Factorize(double number)
        {
            var factors = new List<ulong>();

            var half = Convert.ToUInt64(number / 2);
            var num = Convert.ToUInt64(number);

            while (num > 1)
            {
                for (ulong i = 2; i <= half; i++)
                {
                    if (num % i != 0)
                    {
                        continue;
                    }

                    factors.Add(i);
                    num = Convert.ToUInt64(num / i);
                    break;
                }
            }

            if (factors.Count == 2)
            {
                return new FactorizationResult
                {
                    IsSemiPrime = IsPrime(Convert.ToDouble(factors[0])) && IsPrime(Convert.ToDouble(factors[1])),
                    Factors = factors
                };
            }
            return new FactorizationResult
            {
                IsSemiPrime = false,
                Factors =factors
            };
        }

        public static bool IsPrime(double number)
        {
            var sqrtNumber = Math.Truncate(Math.Sqrt(number));

            var sqrt = Convert.ToUInt64(sqrtNumber);
            var num = Convert.ToUInt64(number);

            for (ulong i = 2; i <= sqrt; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}