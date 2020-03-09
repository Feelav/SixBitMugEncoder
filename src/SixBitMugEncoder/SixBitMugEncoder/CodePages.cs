namespace SixBitMugEncoder
{
    public static class CodePages
    {
        public static char[] CustoBBSmBase64CodePage => new char[] {
            '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
            'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
            ' ', '!', '"', '#', '$', '%', '&', '\'',
            '(', ')', '*', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', ';', ':', '<', '=', '>', '?', (char)21 // default padding character
        };

        internal static char DefaultPaddingCharacter = '|';
    }
}
