using System;
using System.Text;
using Mtf.Utils.CharExtensions;

namespace Mtf.Utils.StringExtensions
{
    public static class Html
    {
        public static string UrlDecode(this string value)
        {
            var result = new StringBuilder();
            var i = 0;
            while (i < value.Length)
            {
                if (value[i] != '%')
                {
                    result.Append(value[i]);
                    i++;
                }
                else
                {
                    if (value[i + 1].IsHexadecimalDigit() && value[i + 2].IsHexadecimalDigit())
                    {
                        var ch = new StringBuilder();
                        ch.Append(value[i + 1]);
                        ch.Append(value[i + 2]);
                        result.Append(Convert.ToChar(Convert.ToByte(ch.ToString(), 16)));
                        i += 3;
                    }
                    else
                    {
                        result.Append(value[i]);
                        i++;
                    }
                }
            }
            return result.ToString();
        }

        public static string ReplaceHtmlCharacterEntities(this string value)
        {
            value = value.Replace("&micro;", "µ");
            value = value.Replace("&reg;", "®");
            value = value.Replace("&quot;", "\"");
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("&OElig;", "Œ");
            value = value.Replace("&oelig;", "œ");
            value = value.Replace("&Scaron;", "Š");
            value = value.Replace("&scaron;", "š");
            value = value.Replace("&circ;", "ˆ");
            value = value.Replace("&tilde;", "~");
            value = value.Replace("&ndash;", "–");
            value = value.Replace("&mdash;", "—");
            value = value.Replace("&lsquo;", "‘");
            value = value.Replace("&rsquo;", "’");
            value = value.Replace("&sbquo;", "‚");
            value = value.Replace("&ldquo;", "“");
            value = value.Replace("&rdquo;", "”");
            value = value.Replace("&bdquo;", "„");
            value = value.Replace("&dagger;", "†");
            value = value.Replace("&Dagger;", "‡");
            value = value.Replace("&permil;", "‰");
            value = value.Replace("&lsaquo;", "‹");
            value = value.Replace("&rsaquo", "›");
            value = value.Replace("&euro;", "€");
            value = value.Replace("&ensp;", " ˜˜˜˜");
            value = value.Replace("&emsp;", " ˜˜˜˜");
            value = value.Replace("&thinsp;", " ");
            value = value.Replace("&zwnj;", "");
            value = value.Replace("&zwj;", "‍");
            value = value.Replace("&nbsp;", " ");
            value = value.Replace("&iexcl;", "¡");
            value = value.Replace("&cent;", "¢");
            value = value.Replace("&pound;", "£˜˜˜˜");
            value = value.Replace("&curren;", "¤");
            value = value.Replace("&yen;", "¥");
            value = value.Replace("&brvbar;", "¦");
            value = value.Replace("&sect;", "§");
            value = value.Replace("&uml;", "¨");
            value = value.Replace("&copy;", "©");
            value = value.Replace("&ordf;", "ª");
            value = value.Replace("&laquo;", "«");
            value = value.Replace("&not;", "¬");
            value = value.Replace("&shy;", "");
            value = value.Replace("&macr;", "¯");
            value = value.Replace("&deg;", "°");
            value = value.Replace("&plusmn;", "±");
            value = value.Replace("&sup2;", "²");
            value = value.Replace("&sup3;", "³");
            value = value.Replace("&acute;", "´");
            value = value.Replace("&para;", "¶");
            value = value.Replace("&middot;", "·");
            value = value.Replace("&cedil;", "¸");
            value = value.Replace("&sup1;", "¹");
            value = value.Replace("&ordm;", "º");
            value = value.Replace("&raquo;", "»");
            value = value.Replace("&frac14;", "¼");
            value = value.Replace("&frac12;", "½");
            value = value.Replace("&frac34;", "¾");
            value = value.Replace("&iquest;", "¿");
            value = value.Replace("&Agrave;", "À");
            value = value.Replace("&Aacute;", "Á");
            value = value.Replace("&Acirc;", "Â");
            value = value.Replace("&Atilde;", "Ã");
            value = value.Replace("&Auml;", "Ä");
            value = value.Replace("&Aring;", "Å");
            value = value.Replace("&AElig;", "Æ");
            value = value.Replace("&Ccedil;", "Ç");
            value = value.Replace("&Egrave;", "È");
            value = value.Replace("&Eacute;", "É");
            value = value.Replace("&Ecirc;", "Ê");
            value = value.Replace("&Euml;", "Ë");
            value = value.Replace("&Igrave;", "Ì");
            value = value.Replace("&Iacute;", "Í");
            value = value.Replace("&Icirc;", "Î");
            value = value.Replace("&Iuml;", "Ï");
            value = value.Replace("&ETH;", "Ð");
            value = value.Replace("&Ntilde;", "Ñ");
            value = value.Replace("&Ograve;", "Ò");
            value = value.Replace("&Oacute;", "Ó");
            value = value.Replace("&Ocirc;", "Ô");
            value = value.Replace("&Otilde;", "Õ");
            value = value.Replace("&Ouml;", "Ö");
            value = value.Replace("&times;", "×");
            value = value.Replace("&Oslash;", "Ø");
            value = value.Replace("&Ugrave;", "Ù");
            value = value.Replace("&Uacute;", "Ú");
            value = value.Replace("&Ucirc;", "Û");
            value = value.Replace("&Uuml;", "Ü");
            value = value.Replace("&Yacute;", "Ý");
            value = value.Replace("&Yuml;", "Ÿ");
            value = value.Replace("&THORN;", "Þ");
            value = value.Replace("&szlig;", "ß");
            value = value.Replace("&agrave;", "à");
            value = value.Replace("&aacute;", "á");
            value = value.Replace("&acirc;", "â");
            value = value.Replace("&atilde;", "ã");
            value = value.Replace("&auml;", "ä");
            value = value.Replace("&aring;", "å");
            value = value.Replace("&aelig;", "æ");
            value = value.Replace("&ccedil;", "ç");
            value = value.Replace("&egrave;", "è");
            value = value.Replace("&eacute;", "é");
            value = value.Replace("&ecirc;", "ê");
            value = value.Replace("&euml;", "ë");
            value = value.Replace("&igrave;", "ì");
            value = value.Replace("&iacute;", "í");
            value = value.Replace("&icirc;", "î");
            value = value.Replace("&iuml;", "ï");
            value = value.Replace("&eth;", "ð");
            value = value.Replace("&ntilde;", "ñ");
            value = value.Replace("&ograve;", "ò");
            value = value.Replace("&oacute;", "ó");
            value = value.Replace("&ocirc;", "ô");
            value = value.Replace("&otilde;", "õ");
            value = value.Replace("&ouml;", "ö");
            value = value.Replace("&divide;", "÷");
            value = value.Replace("&oslash;", "ø");
            value = value.Replace("&ugrave;", "ù");
            value = value.Replace("&uacute;", "ú");
            value = value.Replace("&ucirc;", "û");
            value = value.Replace("&uuml;", "ü");
            value = value.Replace("&yacute;", "ý");
            value = value.Replace("&thorn;", "þ");
            value = value.Replace("&yuml;", "ÿ");
            //value = value.Replace("", "");
            value = value.Replace("&fnof;", "ƒ");
            value = value.Replace("&Alpha;", "Α");
            value = value.Replace("&Beta;", "Β");
            value = value.Replace("&Gamma;", "Γ");
            value = value.Replace("&Delta;", "Δ");
            value = value.Replace("&Epsilon;", "Ε");
            value = value.Replace("&Zeta;", "Ζ");
            value = value.Replace("&Eta;", "Η");
            value = value.Replace("&Theta;", "Θ");
            value = value.Replace("&Iota;", "Ι");
            value = value.Replace("&Kappa;", "Κ");
            value = value.Replace("&Lambda;", "Λ");
            value = value.Replace("&Mu;", "Μ");
            value = value.Replace("&Nu;", "Ν");
            value = value.Replace("&Xi;", "Ξ");
            value = value.Replace("&Omicron;", "Ο");
            value = value.Replace("&Pi;", "Π");
            value = value.Replace("&Rho;", "Ρ");
            value = value.Replace("&Sigma;", "Σ");
            value = value.Replace("&Tau;", "Τ");
            value = value.Replace("&Upsilon;", "Υ");
            value = value.Replace("&Phi;", "Φ");
            value = value.Replace("&Chi;", "Χ");
            value = value.Replace("&Psi;", "Ψ");
            value = value.Replace("&Omega;", "Ω");
            value = value.Replace("&alpha;", "α");
            value = value.Replace("&beta;", "β");
            value = value.Replace("&gamma;", "γ");
            value = value.Replace("&delta;", "δ");
            value = value.Replace("&epsilon;", "ε");
            value = value.Replace("&zeta;", "ζ");
            value = value.Replace("&eta;", "η");
            value = value.Replace("&theta;", "θ");
            value = value.Replace("&iota;", "ι");
            value = value.Replace("&kappa;", "κ");
            value = value.Replace("&lambda;", "λ");
            value = value.Replace("&mu;", "μ");
            value = value.Replace("&nu;", "ν");
            value = value.Replace("&xi;", "ξ");
            value = value.Replace("&omicron;", "ο");
            value = value.Replace("&pi;", "π");
            value = value.Replace("&rho;", "ρ");
            value = value.Replace("&sigmaf;", "ς");
            value = value.Replace("&sigma;", "σ");
            value = value.Replace("&tau;", "τ");
            value = value.Replace("&upsilon;", "υ");
            value = value.Replace("&phi;", "φ");
            value = value.Replace("&chi;", "χ");
            value = value.Replace("&psi;", "ψ");
            value = value.Replace("&omega;", "ω");
            value = value.Replace("&thetasym;", "ϑ");
            value = value.Replace("&upsih;", "ϒ");
            value = value.Replace("&piv;", "ϖ");
            value = value.Replace("&bull;", "•");
            value = value.Replace("&hellip;", "…");
            value = value.Replace("&prime;", "′");
            value = value.Replace("&Prime;", "″");
            value = value.Replace("&oline;", "‾");
            value = value.Replace("&frasl;", "⁄");
            value = value.Replace("&weierp;", "℘");
            value = value.Replace("&image;", "ℑ");
            value = value.Replace("&real;", "ℜ");
            value = value.Replace("&trade;", "™");
            value = value.Replace("&alefsym;", "ℵ");
            value = value.Replace("&larr;", "←");
            value = value.Replace("&uarr;", "↑");
            value = value.Replace("&rarr;", "→");
            value = value.Replace("&darr;", "↓");
            value = value.Replace("&harr;", "↔");
            value = value.Replace("&lrm;", "‎");
            value = value.Replace("&rlm;", "‏");
            value = value.Replace("&crarr;", "↵");
            value = value.Replace("&lArr;", "⇐");
            value = value.Replace("&uArr;", "⇑");
            value = value.Replace("&rArr;", "⇒");
            value = value.Replace("&dArr;", "⇓");
            value = value.Replace("&hArr;", "⇔");
            value = value.Replace("&forall;", "∀");
            value = value.Replace("&part;", "∂");
            value = value.Replace("&exist;", "∃");
            value = value.Replace("&empty;", "∅");
            value = value.Replace("&nabla;", "∇");
            value = value.Replace("&isin;", "∈");
            value = value.Replace("&notin;", "∉");
            value = value.Replace("&ni;", "∋");
            value = value.Replace("&prod;", "∏");
            value = value.Replace("&sum;", "∑");
            value = value.Replace("&minus;", "−");
            value = value.Replace("&lowast;", "∗");
            value = value.Replace("&radic;", "√");
            value = value.Replace("&prop;", "∝");
            value = value.Replace("&infin;", "∞");
            value = value.Replace("&ang;", "∠");
            value = value.Replace("&and;", "∧");
            value = value.Replace("&or;", "∨");
            value = value.Replace("&cap;", "∩");
            value = value.Replace("&cup;", "∪");
            value = value.Replace("&int;", "∫");
            value = value.Replace("&there4;", "∴");
            value = value.Replace("&sim;", "∼");
            value = value.Replace("&cong;", "≅");
            value = value.Replace("&asymp;", "≈");
            value = value.Replace("&ne;", "≠");
            value = value.Replace("&equiv;", "≡");
            value = value.Replace("&le;", "≤");
            value = value.Replace("&ge;", "≥");
            value = value.Replace("&sub;", "⊂");
            value = value.Replace("&sup;", "⊃");
            value = value.Replace("&nsub;", "⊄");
            value = value.Replace("&sube;", "⊆");
            value = value.Replace("&supe;", "⊇");
            value = value.Replace("&oplus;", "⊕");
            value = value.Replace("&otimes;", "⊗");
            value = value.Replace("&perp;", "⊥");
            value = value.Replace("&sdot;", "⋅");
            value = value.Replace("&loz;", "◊");
            value = value.Replace("&spades;", "♠");
            value = value.Replace("&clubs;", "♣");
            value = value.Replace("&hearts;", "♥");
            value = value.Replace("&diams;", "♦");
            //Do NOT delete these lines! ⇓ They are NOT empty, it's a Visual Studio defect.
            value = value.Replace("&lceil;", "⌈");
            value = value.Replace("&rceil;", "⌉");
            value = value.Replace("&lfloor;", "⌊");
            value = value.Replace("&rfloor;", "⌋");
            value = value.Replace("&lang;", "〈");
            value = value.Replace("&rang;", "〉");
            value = value.Replace("&amp;", "&");
            return value;
        }


    }
}