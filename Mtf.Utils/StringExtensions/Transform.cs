using System.Text;

namespace Mtf.Utils.StringExtensions
{
    public static class Transform
    {
        public static string HungarianToEnglish(string value)
        {
            var sb = new StringBuilder();
            foreach (var ch in value)
            {
                switch (ch)
                {
                    case 'á':
                        sb.Append('a');
                        break;
                    case 'Á':
                        sb.Append('A');
                        break;
                    case 'é':
                        sb.Append('e');
                        break;
                    case 'É':
                        sb.Append('E');
                        break;
                    case 'í':
                        sb.Append('i');
                        break;
                    case 'Í':
                        sb.Append('I');
                        break;
                    case 'ó':
                    case 'ö':
                    case 'ő':
                        sb.Append('o');
                        break;
                    case 'Ó':
                    case 'Ö':
                    case 'Ő':
                        sb.Append('O');
                        break;
                    case 'ú':
                    case 'ü':
                    case 'ű':
                        sb.Append('u');
                        break;
                    case 'Ú':
                    case 'Ü':
                    case 'Ű':
                        sb.Append('U');
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
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
    }
}