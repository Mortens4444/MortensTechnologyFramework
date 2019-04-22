using System;
using System.Collections.Generic;
using System.Linq;

namespace Mtf.Utils.CharExtensions
{
    public static class BaseExtensions
    {
        public static bool IsAnyOf(this char value, params char[] chars)
        {
            return chars.Any(ch => ch == value);
        }

        public static bool IsDigit(this char value)
        {
            return Char.IsDigit(value);
        }

        public static bool IsControl(this char value)
        {
            return Char.IsControl(value);
        }

        public static bool IsHighSurrogate(this char value)
        {
            return Char.IsHighSurrogate(value);
        }

        public static bool IsLetter(this char value)
        {
            return Char.IsLetter(value);
        }

        public static bool IsLetterOrDigit(this char value)
        {
            return Char.IsLetterOrDigit(value);
        }

        public static bool IsLower(this char value)
        {
            return Char.IsLower(value);
        }

        public static bool IsLowSurrogate(this char value)
        {
            return Char.IsLowSurrogate(value);
        }

        public static bool IsNumber(this char value)
        {
            return Char.IsNumber(value);
        }

        public static bool IsPunctuation(this char value)
        {
            return Char.IsPunctuation(value);
        }

        public static bool IsSeparator(this char value)
        {
            return Char.IsSeparator(value);
        }

        public static bool IsSurrogate(this char value)
        {
            return Char.IsSurrogate(value);
        }

        public static bool IsSymbol(this char value)
        {
            return Char.IsSymbol(value);
        }

        public static bool IsUpper(this char value)
        {
            return Char.IsUpper(value);
        }

        public static bool IsWhiteSpace(this char value)
        {
            return Char.IsWhiteSpace(value);
        }

        public static readonly Dictionary<char, double> HungarianCharacterStatistics = new Dictionary<char, double>
            {
                { 'a', 8.60741161159006 },
                { 'á', 3.78017633646799 },
                { 'b', 1.6782299811138 },
                { 'c', 0.814161716785236 },
                { 'd', 2.31429382235226 },
                { 'e', 9.90596040747228 },
                { 'é', 2.8984939965359 },
                { 'f', 0.880703780176336 },
                { 'g', 3.66568484504506 },
                { 'h', 1.64887318844125 },
                { 'i', 3.63730661212827 },
                { 'í', 0.310203442573221 },
                { 'j', 1.50111066532278 },
                { 'k', 4.43776849233298 },
                { 'l', 5.89484396864695 },
                { 'm', 3.56880742922567 },
                { 'n', 5.97606442837432 },
                { 'o', 4.13930776682878 },
                { 'ó', 0.730984137546359 },
                { 'ö', 1.10283684473192 },
                { 'ő', 0.684013269270288 },
                { 'p', 0.812204597273733 },
                //{ 'q', 0 },
                { 'r', 4.11484377293499 },
                { 's', 5.37620729809866 },
                { 't', 7.72572927165797 },
                { 'u', 0.912996252116136 },
                { 'ú', 0.211368907242321 },
                { 'ü', 0.394359581567848 },
                { 'ű', 0.143848284095468 },
                { 'v', 2.02659725416133 },
                //{ 'x', 0 },
                { 'y', 2.6059046295662 },
                { 'z', 3.80072609133877 },
                { 'A', 0.376745505964322 },
                { 'Á', 0.0215283146265327 },
                { 'B', 0.155591001164486 },
                { 'C', 0.066542063391101 },
                { 'D', 0.133084126782202 },
                { 'E', 0.21724026577683 },
                { 'É', 0.112534371911421 },
                { 'F', 0.094920296307894 },
                { 'G', 0.0293567926725445 },
                { 'H', 0.313139121840475 },
                { 'I', 0.137976925560959 },
                { 'Í', 0.0166355158477752 },
                { 'J', 0.199626190173303 },
                { 'K', 0.187883473104285 },
                { 'L', 0.116448610934427 },
                { 'M', 0.461880204714701 },
                { 'N', 0.220175945044084 },
                { 'O', 0.0518636670548287 },
                { 'Ó', 0.00684991829026039 },
                { 'ö', 0.0244639938937871 },
                { 'Ő', 0.0127212768247693 },
                { 'P', 0.032292471939799 },
                //{ 'Q', 0 },
                { 'R', 0.0596921451008406 },
                { 'S', 0.302374964527209 },
                { 'T', 0.182990674325528 },
                { 'U', 0.0313139121840475 },
                { 'Ú', 0.00489279877875743 },
                { 'Ü', 0.00489279877875743 },
                //{ 'Ű', 0 },
                { 'V', 0.116448610934427 },
                //{ 'X', 0 },
                //{ 'Y', 0 },
                { 'Z', 0.00587135853450891 }
            };

        public static char GenerateCharByHungarianStatistics()
        {
            var r = new Random(RandomUtils.GetSeed());

            double d = (double)r.Next(0, 1000000000) / 10000000, summ = 0;
            for (var i = 0; i < HungarianCharacterStatistics.Count; i++)
            {
                var keyValuePair = HungarianCharacterStatistics.ElementAt(i);
                summ += keyValuePair.Value;
                if (d < summ)
                {
                    return keyValuePair.Key;
                }
            }
            return ' ';
        }
    }
}