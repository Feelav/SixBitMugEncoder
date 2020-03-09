using System;
using Xunit;
using SixBitMugEncoder;

namespace SixBitMugEncoderTest
{
    public class MugEncoderText
    {

        private static readonly string testHex_1 = "3464a02f0c36e2a4c54a0cf2c37d2dcb0dea40e3e0c30c30cb1cf6a95243831a90114831c3234bc82060401620413835652a84346832c31d70d32c6a2430e0db3c37cb0a85610832c32c30d32c400d6f";
        private static readonly string testCustomBase64_1 = "MFR K0068*SER 32074-207*PNO 00002136*UIC 1*PDT 102MK2BA PAX PS 5YR*DMF 20150421*ICC 630720*EXP 20200421@CV<|";
        private static readonly string testHex_2 = "CF2C37D2DCB0E000";
        private static readonly string testCustomBase64_2 = "32074-208@@|";
        [Fact]
        public void HexToCustomBase64()
        {
            var encoder = new MugEncoder();
            var customBase64_1 = encoder.ToCustomBase64(testHex_1);
            var customBase64_2 = encoder.ToCustomBase64(testHex_2);

            Assert.Equal(testCustomBase64_1, customBase64_1);
            Assert.Equal(testCustomBase64_2, customBase64_2);
        }

        [Fact]
        public void HexToCustomBase64Invalid()
        {
            var hex = "g53464a0";
            var encoder = new MugEncoder();
            string customBase64;

            Assert.Throws<ArgumentException>(() => customBase64 = encoder.ToCustomBase64(hex));
        }

        [Fact]
        public void HexToCustomBase64InvalidNull()
        {
            string hex = null;
            var encoder = new MugEncoder();
            string customBase64;

            Assert.Throws<ArgumentNullException>(() => customBase64 = encoder.ToCustomBase64(hex));
        }
    }
}
