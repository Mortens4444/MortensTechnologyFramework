﻿using System.Text;

namespace Mtf.Cryptography
{
    public static class CharCoding
    {
        public static string GetChar(byte[] str)
        {
            var result = new StringBuilder();

            foreach (var ch in str)
            {
                switch (ch)
                {
                    case 0: result.Append("<NUL = 0x00>");
                        break;
                    case 1: result.Append("<SOH = 0x01>");
                        break;
                    case 2: result.Append("<STX = 0x02>");
                        break;
                    case 3: result.Append("<ETX = 0x03>");
                        break;
                    case 4: result.Append("<EOT = 0x04>");
                        break;
                    case 5: result.Append("<ENQ = 0x05>");
                        break;
                    case 6: result.Append("<ACK = 0x06>");
                        break;
                    case 7: result.Append("<BEL = 0x07>");
                        break;
                    case 8: result.Append("<BS = 0x08>");
                        break;
                    case 9: result.Append("<TAB = 0x09>");
                        break;
                    case 10: result.Append("<LF = 0x0A>");
                        break;
                    case 11: result.Append("<VT = 0x0B>");
                        break;
                    case 12: result.Append("<FF = 0x0C>");
                        break;
                    case 13: result.Append("<CR = 0x0D>");
                        break;
                    case 14: result.Append("<SO = 0x0E>");
                        break;
                    case 15: result.Append("<SI = 0x0F>");
                        break;
                    case 16: result.Append("<DLE = 0x10>");
                        break;
                    case 17: result.Append("<DC1 = 0x11>");
                        break;
                    case 18: result.Append("<DC2 = 0x12>");
                        break;
                    case 19: result.Append("<DC3 = 0x13>");
                        break;
                    case 20: result.Append("<DC4 = 0x14>");
                        break;
                    case 21: result.Append("<NAK = 0x15>");
                        break;
                    case 22: result.Append("<SYN = 0x16>");
                        break;
                    case 23: result.Append("<ETB = 0x17>");
                        break;
                    case 24: result.Append("<CAN = 0x18>");
                        break;
                    case 25: result.Append("<EM = 0x19>");
                        break;
                    case 26: result.Append("<SUB = 0x1A>");
                        break;
                    case 27: result.Append("<ESC = 0x1B>");
                        break;
                    case 28: result.Append("<FS = 0x1C>");
                        break;
                    case 29: result.Append("<GS = 0x1D>");
                        break;
                    case 30: result.Append("<RS = 0x1E>");
                        break;
                    case 31: result.Append("<US = 0x1F>");
                        break;
                    default: result.Append(Encoding.UTF8.GetChars(new[] { ch }));
                        break;
                }
            }

            return result.ToString();
        }

        public static string GetSpecialStringEnglishEqvivalent(string str)
        {
            const string specials = "áâäąăÁÂÄĄĂßçčćÇČĆďđĎĐęéëěĘÉËĚíîÍÎĺľłĹĽŁóöőôÓÖŐÔµňńŇŃŕřŔŘşšśŞŠŚţťŢŤúůüűÚŮÜŰýÝźżžŹŻŽ";
            var sb = new StringBuilder();
            var i = 0;

            while (i < str.Length)
            {
                var index = specials.IndexOf(str[i]);
                switch (index)
                {
                    case -1: sb.Append(str[i]); break;
                    case 0: case 1: case 2: case 3: case 4: sb.Append('a'); break;
                    case 5: case 6: case 7: case 8: case 9: sb.Append('A'); break;
                    case 10: sb.Append("ss"); break;
                    case 11: case 12: case 13: sb.Append('c'); break;
                    case 14: case 15: case 16: sb.Append('C'); break;
                    case 17: case 18: sb.Append('d'); break;
                    case 19: case 20: sb.Append('D'); break;
                    case 21: case 22: case 23: case 24: sb.Append('e'); break;
                    case 25: case 26: case 27: case 28: sb.Append('E'); break;
                    case 29: case 30: sb.Append('i'); break;
                    case 31: case 32: sb.Append('I'); break;
                    case 33: case 34: case 35: sb.Append('l'); break;
                    case 36: case 37: case 38: sb.Append('L'); break;
                    case 39: case 40: case 41: case 42: sb.Append('o'); break;
                    case 43: case 44: case 45: case 46: sb.Append('O'); break;
                    case 47: sb.Append('u'); break;
                    case 48: case 49: sb.Append('n'); break;
                    case 50: case 51: sb.Append('N'); break;
                    case 52: case 53: sb.Append('r'); break;
                    case 54: case 55: sb.Append('R'); break;
                    case 56: case 57: case 58: sb.Append('s'); break;
                    case 59: case 60: case 61: sb.Append('S'); break;
                    case 62: case 63: sb.Append('t'); break;
                    case 64: case 65: sb.Append('T'); break;
                    case 66: case 67: case 68: case 69: sb.Append('u'); break;
                    case 70: case 71: case 72: case 73: sb.Append('U'); break;
                    case 74: sb.Append('y'); break;
                    case 75: sb.Append('Y'); break;
                    case 76: case 77: case 78: sb.Append('z'); break;
                    case 79: case 80: case 81: sb.Append('Z'); break;
                    default: sb.Append(str[i]); break;
                }
                i++;
            }
            return sb.ToString();
        }

        public static string ReplaceHtmlCharacterEntities(string text)
        {
            text = text.Replace("&micro;", "µ");
            text = text.Replace("&reg;", "®");
            text = text.Replace("&quot;", "\"");
            text = text.Replace("&lt;", "<");
            text = text.Replace("&gt;", ">");
            text = text.Replace("&OElig;", "Œ");
            text = text.Replace("&oelig;", "œ");
            text = text.Replace("&Scaron;", "Š");
            text = text.Replace("&scaron;", "š");
            text = text.Replace("&circ;", "ˆ");
            text = text.Replace("&tilde;", "~");
            text = text.Replace("&ndash;", "–");
            text = text.Replace("&mdash;", "—");
            text = text.Replace("&lsquo;", "‘");
            text = text.Replace("&rsquo;", "’");
            text = text.Replace("&sbquo;", "‚");
            text = text.Replace("&ldquo;", "“");
            text = text.Replace("&rdquo;", "”");
            text = text.Replace("&bdquo;", "„");
            text = text.Replace("&dagger;", "†");
            text = text.Replace("&Dagger;", "‡");
            text = text.Replace("&permil;", "‰");
            text = text.Replace("&lsaquo;", "‹");
            text = text.Replace("&rsaquo", "›");
            text = text.Replace("&euro;", "€");
            text = text.Replace("&ensp;", " ˜˜˜˜");
            text = text.Replace("&emsp;", " ˜˜˜˜");
            text = text.Replace("&thinsp;", " ");
            text = text.Replace("&zwnj;", "");
            text = text.Replace("&zwj;", "‍");
            text = text.Replace("&nbsp;", " ");
            text = text.Replace("&iexcl;", "¡");
            text = text.Replace("&cent;", "¢");
            text = text.Replace("&pound;", "£˜˜˜˜");
            text = text.Replace("&curren;", "¤");
            text = text.Replace("&yen;", "¥");
            text = text.Replace("&brvbar;", "¦");
            text = text.Replace("&sect;", "§");
            text = text.Replace("&uml;", "¨");
            text = text.Replace("&copy;", "©");
            text = text.Replace("&ordf;", "ª");
            text = text.Replace("&laquo;", "«");
            text = text.Replace("&not;", "¬");
            text = text.Replace("&shy;", "");
            text = text.Replace("&macr;", "¯");
            text = text.Replace("&deg;", "°");
            text = text.Replace("&plusmn;", "±");
            text = text.Replace("&sup2;", "²");
            text = text.Replace("&sup3;", "³");
            text = text.Replace("&acute;", "´");
            text = text.Replace("&para;", "¶");
            text = text.Replace("&middot;", "·");
            text = text.Replace("&cedil;", "¸");
            text = text.Replace("&sup1;", "¹");
            text = text.Replace("&ordm;", "º");
            text = text.Replace("&raquo;", "»");
            text = text.Replace("&frac14;", "¼");
            text = text.Replace("&frac12;", "½");
            text = text.Replace("&frac34;", "¾");
            text = text.Replace("&iquest;", "¿");
            text = text.Replace("&Agrave;", "À");
            text = text.Replace("&Aacute;", "Á");
            text = text.Replace("&Acirc;", "Â");
            text = text.Replace("&Atilde;", "Ã");
            text = text.Replace("&Auml;", "Ä");
            text = text.Replace("&Aring;", "Å");
            text = text.Replace("&AElig;", "Æ");
            text = text.Replace("&Ccedil;", "Ç");
            text = text.Replace("&Egrave;", "È");
            text = text.Replace("&Eacute;", "É");
            text = text.Replace("&Ecirc;", "Ê");
            text = text.Replace("&Euml;", "Ë");
            text = text.Replace("&Igrave;", "Ì");
            text = text.Replace("&Iacute;", "Í");
            text = text.Replace("&Icirc;", "Î");
            text = text.Replace("&Iuml;", "Ï");
            text = text.Replace("&ETH;", "Ð");
            text = text.Replace("&Ntilde;", "Ñ");
            text = text.Replace("&Ograve;", "Ò");
            text = text.Replace("&Oacute;", "Ó");
            text = text.Replace("&Ocirc;", "Ô");
            text = text.Replace("&Otilde;", "Õ");
            text = text.Replace("&Ouml;", "Ö");
            text = text.Replace("&times;", "×");
            text = text.Replace("&Oslash;", "Ø");
            text = text.Replace("&Ugrave;", "Ù");
            text = text.Replace("&Uacute;", "Ú");
            text = text.Replace("&Ucirc;", "Û");
            text = text.Replace("&Uuml;", "Ü");
            text = text.Replace("&Yacute;", "Ý");
            text = text.Replace("&Yuml;", "Ÿ");
            text = text.Replace("&THORN;", "Þ");
            text = text.Replace("&szlig;", "ß");
            text = text.Replace("&agrave;", "à");
            text = text.Replace("&aacute;", "á");
            text = text.Replace("&acirc;", "â");
            text = text.Replace("&atilde;", "ã");
            text = text.Replace("&auml;", "ä");
            text = text.Replace("&aring;", "å");
            text = text.Replace("&aelig;", "æ");
            text = text.Replace("&ccedil;", "ç");
            text = text.Replace("&egrave;", "è");
            text = text.Replace("&eacute;", "é");
            text = text.Replace("&ecirc;", "ê");
            text = text.Replace("&euml;", "ë");
            text = text.Replace("&igrave;", "ì");
            text = text.Replace("&iacute;", "í");
            text = text.Replace("&icirc;", "î");
            text = text.Replace("&iuml;", "ï");
            text = text.Replace("&eth;", "ð");
            text = text.Replace("&ntilde;", "ñ");
            text = text.Replace("&ograve;", "ò");
            text = text.Replace("&oacute;", "ó");
            text = text.Replace("&ocirc;", "ô");
            text = text.Replace("&otilde;", "õ");
            text = text.Replace("&ouml;", "ö");
            text = text.Replace("&divide;", "÷");
            text = text.Replace("&oslash;", "ø");
            text = text.Replace("&ugrave;", "ù");
            text = text.Replace("&uacute;", "ú");
            text = text.Replace("&ucirc;", "û");
            text = text.Replace("&uuml;", "ü");
            text = text.Replace("&yacute;", "ý");
            text = text.Replace("&thorn;", "þ");
            text = text.Replace("&yuml;", "ÿ");
            text = text.Replace("&fnof;", "ƒ");
            text = text.Replace("&Alpha;", "Α");
            text = text.Replace("&Beta;", "Β");
            text = text.Replace("&Gamma;", "Γ");
            text = text.Replace("&Delta;", "Δ");
            text = text.Replace("&Epsilon;", "Ε");
            text = text.Replace("&Zeta;", "Ζ");
            text = text.Replace("&Eta;", "Η");
            text = text.Replace("&Theta;", "Θ");
            text = text.Replace("&Iota;", "Ι");
            text = text.Replace("&Kappa;", "Κ");
            text = text.Replace("&Lambda;", "Λ");
            text = text.Replace("&Mu;", "Μ");
            text = text.Replace("&Nu;", "Ν");
            text = text.Replace("&Xi;", "Ξ");
            text = text.Replace("&Omicron;", "Ο");
            text = text.Replace("&Pi;", "Π");
            text = text.Replace("&Rho;", "Ρ");
            text = text.Replace("&Sigma;", "Σ");
            text = text.Replace("&Tau;", "Τ");
            text = text.Replace("&Upsilon;", "Υ");
            text = text.Replace("&Phi;", "Φ");
            text = text.Replace("&Chi;", "Χ");
            text = text.Replace("&Psi;", "Ψ");
            text = text.Replace("&Omega;", "Ω");
            text = text.Replace("&alpha;", "α");
            text = text.Replace("&beta;", "β");
            text = text.Replace("&gamma;", "γ");
            text = text.Replace("&delta;", "δ");
            text = text.Replace("&epsilon;", "ε");
            text = text.Replace("&zeta;", "ζ");
            text = text.Replace("&eta;", "η");
            text = text.Replace("&theta;", "θ");
            text = text.Replace("&iota;", "ι");
            text = text.Replace("&kappa;", "κ");
            text = text.Replace("&lambda;", "λ");
            text = text.Replace("&mu;", "μ");
            text = text.Replace("&nu;", "ν");
            text = text.Replace("&xi;", "ξ");
            text = text.Replace("&omicron;", "ο");
            text = text.Replace("&pi;", "π");
            text = text.Replace("&rho;", "ρ");
            text = text.Replace("&sigmaf;", "ς");
            text = text.Replace("&sigma;", "σ");
            text = text.Replace("&tau;", "τ");
            text = text.Replace("&upsilon;", "υ");
            text = text.Replace("&phi;", "φ");
            text = text.Replace("&chi;", "χ");
            text = text.Replace("&psi;", "ψ");
            text = text.Replace("&omega;", "ω");
            text = text.Replace("&thetasym;", "ϑ");
            text = text.Replace("&upsih;", "ϒ");
            text = text.Replace("&piv;", "ϖ");
            text = text.Replace("&bull;", "•");
            text = text.Replace("&hellip;", "…");
            text = text.Replace("&prime;", "′");
            text = text.Replace("&Prime;", "″");
            text = text.Replace("&oline;", "‾");
            text = text.Replace("&frasl;", "⁄");
            text = text.Replace("&weierp;", "℘");
            text = text.Replace("&image;", "ℑ");
            text = text.Replace("&real;", "ℜ");
            text = text.Replace("&trade;", "™");
            text = text.Replace("&alefsym;", "ℵ");
            text = text.Replace("&larr;", "←");
            text = text.Replace("&uarr;", "↑");
            text = text.Replace("&rarr;", "→");
            text = text.Replace("&darr;", "↓");
            text = text.Replace("&harr;", "↔");
            text = text.Replace("&lrm;", "‎");
            text = text.Replace("&rlm;", "‏");
            text = text.Replace("&crarr;", "↵");
            text = text.Replace("&lArr;", "⇐");
            text = text.Replace("&uArr;", "⇑");
            text = text.Replace("&rArr;", "⇒");
            text = text.Replace("&dArr;", "⇓");
            text = text.Replace("&hArr;", "⇔");
            text = text.Replace("&forall;", "∀");
            text = text.Replace("&part;", "∂");
            text = text.Replace("&exist;", "∃");
            text = text.Replace("&empty;", "∅");
            text = text.Replace("&nabla;", "∇");
            text = text.Replace("&isin;", "∈");
            text = text.Replace("&notin;", "∉");
            text = text.Replace("&ni;", "∋");
            text = text.Replace("&prod;", "∏");
            text = text.Replace("&sum;", "∑");
            text = text.Replace("&minus;", "−");
            text = text.Replace("&lowast;", "∗");
            text = text.Replace("&radic;", "√");
            text = text.Replace("&prop;", "∝");
            text = text.Replace("&infin;", "∞");
            text = text.Replace("&ang;", "∠");
            text = text.Replace("&and;", "∧");
            text = text.Replace("&or;", "∨");
            text = text.Replace("&cap;", "∩");
            text = text.Replace("&cup;", "∪");
            text = text.Replace("&int;", "∫");
            text = text.Replace("&there4;", "∴");
            text = text.Replace("&sim;", "∼");
            text = text.Replace("&cong;", "≅");
            text = text.Replace("&asymp;", "≈");
            text = text.Replace("&ne;", "≠");
            text = text.Replace("&equiv;", "≡");
            text = text.Replace("&le;", "≤");
            text = text.Replace("&ge;", "≥");
            text = text.Replace("&sub;", "⊂");
            text = text.Replace("&sup;", "⊃");
            text = text.Replace("&nsub;", "⊄");
            text = text.Replace("&sube;", "⊆");
            text = text.Replace("&supe;", "⊇");
            text = text.Replace("&oplus;", "⊕");
            text = text.Replace("&otimes;", "⊗");
            text = text.Replace("&perp;", "⊥");
            text = text.Replace("&sdot;", "⋅");
            text = text.Replace("&loz;", "◊");
            text = text.Replace("&spades;", "♠");
            text = text.Replace("&clubs;", "♣");
            text = text.Replace("&hearts;", "♥");
            text = text.Replace("&diams;", "♦");
            text = text.Replace("&amp;", "&");
            // Do NOT delete these lines! ⇓ They are NOT empty, it's a Visual Studio defect.
            text = text.Replace("&lceil;", "⌈");
            text = text.Replace("&rceil;", "⌉");
            text = text.Replace("&lfloor;", "⌊");
            text = text.Replace("&rfloor;", "⌋");
            text = text.Replace("&lang;", "〈");
            text = text.Replace("&rang;", "〉");
            return text;
        }
    }
}