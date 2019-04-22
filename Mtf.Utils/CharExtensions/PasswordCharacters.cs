namespace Mtf.Utils.CharExtensions
{
    public class PasswordCharacters
    {
        public static readonly char[] BinaryNumbers = { '0', '1' };
        public static readonly char[] DecimalNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static readonly char[] hexadecimal_Numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        public static readonly char[] HEXADECIMAL_Numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static readonly char[] Hexadecimal_Numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static readonly char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static readonly char[] ALPHABET = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static readonly char[] Alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static readonly char[] AlphabetAndDecimalNumbers = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static readonly char[] HungarianAlphabetAndDecimalNumbers = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'á', 'é', 'í', 'ó', 'ö', 'ő', 'ú', 'ü', 'ű', 'Á', 'É', 'Í', 'Ó', 'Ö', 'Ő', 'Ú', 'Ü', 'Ű' };
        public static readonly char[] SpecialCharacters = { ' ', '§', '\'', '"', '+', '!', '%', '/', '=', '(', ')', '~', 'ˇ', '^', '˘', '°', '˛', '`', '˙', '´', '˝', '¨', '¸', '÷', '×', '€', '$', '¤', ',', '.', '-', ';', '>', '*', '<', '{', '}', '[', ']', '?', ':', '_', '#', '&', '@', '\\', '|' };
        public static readonly char[] hungarian_Letters = { 'á', 'é', 'í', 'ó', 'ö', 'ő', 'ú', 'ü', 'ű' };
        public static readonly char[] HUNGARIAN_Letters = { 'Á', 'É', 'Í', 'Ó', 'Ö', 'Ő', 'Ú', 'Ü', 'Ű' };
        public static readonly char[] Hungarian_Letters = { 'á', 'é', 'í', 'ó', 'ö', 'ő', 'ú', 'ü', 'ű', 'Á', 'É', 'Í', 'Ó', 'Ö', 'Ő', 'Ú', 'Ü', 'Ű' };
        public static readonly char[] National_Letters = { 'ä', 'Ä', 'â', 'Â', 'ą', 'Ą', 'ă', 'Ă', 'ß', 'č', 'Č', 'ć', 'Ć', 'ç', 'Ç', 'đ', 'Đ', 'ď', 'Ď', 'ě', 'Ě', 'ę', 'Ę', 'ë', 'Ë', 'î', 'Î', 'ł', 'Ł', 'ĺ', 'Ĺ', 'ľ', 'Ľ', 'ń', 'Ń', 'ň', 'Ň', 'ô', 'Ô', 'ŕ', 'Ŕ', 'ř', 'Ř', 'š', 'Š', 'ş', 'Ş', 'ś', 'Ś', 'ť', 'Ť', 'ţ', 'Ţ', 'ů', 'Ů', 'ý', 'Ý', 'ż', 'Ż', 'ž', 'Ž', 'ź', 'Ź' };
        public static readonly char[] Visible = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'á', 'é', 'í', 'ó', 'ö', 'ő', 'ú', 'ü', 'ű', 'Á', 'É', 'Í', 'Ó', 'Ö', 'Ő', 'Ú', 'Ü', 'Ű', 'ä', 'Ä', 'â', 'Â', 'ą', 'Ą', 'ă', 'Ă', 'ß', 'č', 'Č', 'ć', 'Ć', 'ç', 'Ç', 'đ', 'Đ', 'ď', 'Ď', 'ě', 'Ě', 'ę', 'Ę', 'ë', 'Ë', 'î', 'Î', 'ł', 'Ł', 'ĺ', 'Ĺ', 'ľ', 'Ľ', 'ń', 'Ń', 'ň', 'Ň', 'ô', 'Ô', 'ŕ', 'Ŕ', 'ř', 'Ř', 'š', 'Š', 'ş', 'Ş', 'ś', 'Ś', 'ť', 'Ť', 'ţ', 'Ţ', 'ů', 'Ů', 'ý', 'Ý', 'ż', 'Ż', 'ž', 'Ž', 'ź', 'Ź', ' ', '§', '\'', '"', '+', '!', '%', '/', '=', '(', ')', '~', 'ˇ', '^', '˘', '°', '˛', '`', '˙', '´', '˝', '¨', '¸', '÷', '×', '€', '$', '¤', ',', '.', '-', ';', '>', '*', '<', '{', '}', '[', ']', '?', ':', '_', '#', '&', '@', '\\', '|' };
        public static readonly char[] PasswordGeneratorCharacters = { '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public static readonly char[] Vowels = { 'a', 'A', 'á', 'Á', 'e', 'E', 'é', 'É', 'i', 'I', 'í', 'Í', 'o', 'O', 'ó', 'Ó', 'ö', 'Ö', 'ő', 'Ő', 'u', 'U', 'ú', 'Ú', 'ü', 'Ü', 'ű', 'Ű' };
        public static readonly char[] EnglishVowels = { 'a', 'A', 'e', 'E', 'i', 'I', 'o', 'O', 'u', 'U' };
        public static readonly char[] Consonant = { 'b', 'B', 'c', 'C', 'd', 'D', 'f', 'F', 'g', 'G', 'h', 'H', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };

        public static readonly char[] BadPasswordChar = { '0', 'O', 'I', 'l' };
    }
}