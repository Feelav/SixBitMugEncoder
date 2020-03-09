using System;
using System.Globalization;
using System.Linq;

namespace SixBitMugEncoder
{
    public interface IMugEncoder
    {
        string ToCustomBase64(string hexValue);
    }

    public class MugEncoder : IMugEncoder
    {

        private static char[] CodePage;

        public MugEncoder(char[] codePage = null, char? paddingCharacter = null)
        {
            if(codePage.Length != 65) throw new ArgumentException("CodePage error: Invalid count of characters in codePage.");

            if (paddingCharacter != null & codePage.Any(x => x == paddingCharacter)) throw new ArgumentException("Padding cahracter error: character contained in the codePage.");

            CodePage = codePage ?? CodePages.CustoBBSmBase64CodePage;
            CodePage[64] = paddingCharacter ?? CodePages.DefaultPaddingCharacter;
        }

        public string ToCustomBase64(string hexValue)
        {
            _ = hexValue ?? throw new ArgumentNullException(nameof(hexValue));

            var inData = HexToBytes(hexValue);
            char[] base64 = CodePage;
            int lengthmod3 = inData.Length % 3;
            int calcLength = inData.Length - lengthmod3;
            char[] outChars = new char[calcLength / 3 * 4 + (lengthmod3 == 2 || lengthmod3 == 1 ? 4 : 0)];
            int i;
            int j = 0;

            for (i = 0; i < calcLength; i += 3)
            {
                outChars[j] = base64[(inData[i] & 0xfc) >> 2];
                outChars[j + 1] = base64[((inData[i] & 0x03) << 4) | ((inData[i + 1] & 0xf0) >> 4)];
                outChars[j + 2] = base64[((inData[i + 1] & 0x0f) << 2) | ((inData[i + 2] & 0xc0) >> 6)];
                outChars[j + 3] = base64[inData[i + 2] & 0x3f];
                j += 4;
            }

            i = calcLength;

            switch (lengthmod3)
            {
                case 2: // One character padding needed
                    outChars[j] = base64[(inData[i] & 0xfc) >> 2];
                    outChars[j + 1] = base64[((inData[i] & 0x03) << 4) | ((inData[i + 1] & 0xf0) >> 4)];
                    outChars[j + 2] = base64[(inData[i + 1] & 0x0f) << 2];
                    outChars[j + 3] = base64[64];
                    break;
                case 1: // Two character padding needed
                    outChars[j] = base64[(inData[i] & 0xfc) >> 2];
                    outChars[j + 1] = base64[(inData[i] & 0x03) << 4];
                    outChars[j + 2] = base64[64]; // Pad
                    outChars[j + 3] = base64[64]; // Pad
                    break;
            }

            return new string(outChars);
        }

        private static byte[] HexToBytes(string hex)
        {
            return FromBinHexString(hex);
        }

        internal static byte[] FromBinHexString(string value)
        {
            char[] chars = value.ToCharArray();
            byte[] buffer = new byte[chars.Length / 2 + chars.Length % 2];
            int charLength = chars.Length;

            if (charLength % 2 != 0)
                throw new ArgumentException(value);

            int bufIndex = 0;
            for (int i = 0; i < charLength - 1; i += 2)
            {
                buffer[bufIndex] = FromHex(chars[i], value);
                buffer[bufIndex] <<= 4;
                buffer[bufIndex] += FromHex(chars[i + 1], value);
                bufIndex++;
            }
            return buffer;
        }

        static byte FromHex(char hexDigit, string value)
        {
            try
            {
                return byte.Parse(hexDigit.ToString(CultureInfo.InvariantCulture),
                    NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException(value);
            }
        }
    }
}
