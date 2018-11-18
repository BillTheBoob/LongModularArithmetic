using System;
using NUnit.Framework;
using LongModArithmetic;

namespace LongModArithmetics.Tests
{
    [TestFixture]
    public class ModCalculatorTests
    {
        [Test]
        [TestCase("FFAAA242432FFFFFFF12352565897",45, "7FD551212197FFFFFF")]
        [TestCase("FFFFFFFF", 1 ,"7FFFFFFF")]
        [TestCase("AADDDDDDFFFF3128898923482954398923479827FFFFF", 123, "155BBBBBBFFFE62")]
        [TestCase("F423FFFFFCE22", 4, "F423FFFFFCE2")]
        [TestCase("118427B3B467203A97DE75CF815074515C3E3D1944A5192", 64, "118427B3B467203A97DE75CF8150745")]

        public void TestShiftBitsToLow(string hex, int shift ,string expected)
        {
            var a = new Number(hex);
            var calculator = new Calculator();
            Assert.AreEqual(expected, calculator.ShiftBitsToLow(a,shift).ToString());
        }

        [Test]
        [TestCase("3DFE932067AFAA31440188FA1A058D89E554EB475B2C", "AB45811C4601F13B", "48246FE994E00ABB")]
        [TestCase("380E49493F44D251CBC32D1DA04E5ED5035BAE51BF32D9F1B0E08C595BE77059A6F5C27DD7B63F015D4C14583", "CDACD5559953838CDD2BF714CC57E81347E86B3DB63EE", "2AA56B63F20F333197316F400656D6FA558F6B4D00363")]
        [TestCase("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", "F", "0")]
        public void TestMod(string hex1, string hex2, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            ModCalculator modcalculator = new ModCalculator();
            Assert.AreEqual(expected,modcalculator.Mod(a,b).ToString());
        }


        [Test]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "48C1B463F2782F60D0", "1")]
        [TestCase("A","5","5")]
        [TestCase("1DF9E9A", "4", "2")]
        public void TestGCD(string hex1, string hex2, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            ModCalculator modcalculator = new ModCalculator();
            Assert.AreEqual(expected, modcalculator.GCD(a, b).ToString());
        }
        
        [Test]
        [TestCase("4D0FFA257CCEA11EBAB1F01E65A77392D01F1", "48C1B463F2782F60D0", "1")]
        [TestCase("A", "5", "5")]
        [TestCase("1DF9E9A", "4", "2")]
        [TestCase("8", "4", "4")]
        [TestCase("FFFFFF", "7", "7")]
        [TestCase("5", "3", "1")]
        public void TestSteinGCD(string hex1, string hex2, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            ModCalculator modcalculator = new ModCalculator();
            Assert.AreEqual(expected, modcalculator.SteinGCD(a, b).ToString());
        }

        [Test]
        [TestCase("7", "9")]
        [TestCase("3F2782F60D0AD", "40DB632530375")]
        [TestCase("20AAA555295555205FFF800", "7D63586478A0AAE4FFC4E4A")]
        [TestCase("554AA9F25555555AAA94A5295", "C018049F87D2F6EF43511D996")]
        [TestCase("80202554475555154545555A552A2", "1FF7F8AF19CAE10DEA9DE71CCF3110")]
        public void TestMCalculator(string hex1, string expected)
        {
            var a = new Number(hex1);
            ModCalculator modcalculator = new ModCalculator();
            Assert.AreEqual(expected, modcalculator.MCalculation(a).ToString());
        }



      
      

        [Test]
        [TestCase("2BDC8D2D7DC4A0531CC7F391B4DAD2D99E20F03E3B0DA1CBF7F89AABAA6D5E84E5ABD5359CD7F12A948747A34076DB7F8E5F01706C489A8B7DE9AA157A383BD47A0EF44688FF88717CC8D0FBF3BA49368486BDE1915DEFE1916A4FA67C26AB25B408642ACC7EA8771CAFE4892F450845C8788038F50B7CBC9716FF6CD7466625",
                  "7", "3")]
        [TestCase("D66CCA25D693363EA73F4CF9C0003A80F77ADD6CA63CACD051CA6A6EC576EFA0EE1983BBDB1B6AB0D72E1778EBE1B287B90242125BB252D2F111A3B1C8369AAC7B697594636C55AD48D37A06F6944CC6A4F2825DC920AF7320DDF7C4F10DE36331BAD51B2272D2914AD1AD02C9527082325177A0AC272C6DEE89589F1236D0512976496ED2BEB12EDD4BC409D4628E24ED5A0A05A08A8793EBFCAEE4A33997BEB046E1E04E93C4CDC9AD7A8A925C17C0658BB482980398042A8B0286D948CFDC4ABDA30B07060A15DEF19E3B32F0F41AC2FE91FA49583ECE35B4F74A194B5C57E389C90FABB437859A8E71FC7473802D26D8572EC75E409D19410F30DFDDE220",
                 "7", "1")]
        [TestCase("D66CCA25D693363EA73F4CF9C0003A80F77ADD6CA63CACD051CA6A6EC576EFA0EE1983BBDB1B6AB0D72E1778EBE1B287B90242125BB252D2F111A3B1C8369AAC7B697594636C55AD48D37A06F6944CC6A4F2825DC920AF7320DDF7C4F10DE36331BAD51B2272D2914AD1AD02C9527082325177A0AC272C6DEE89589F1236D0512976496ED2BEB12EDD4BC409D4628E24ED5A0A05A08A8793EBFCAEE4A33997BEB046E1E04E93C4CDC9AD7A8A925C17C0658BB482980398042A8B0286D948CFDC4ABDA30B07060A15DEF19E3B32F0F41AC2FE91FA49583ECE35B4F74A194B5C57E389C90FABB437859A8E71FC7473802D26D8572EC75E409D19410F30DFDDE220",
                  "2", "0")]
        [TestCase("FF", "FF", "0")]
        [TestCase("5", "3", "2")]
        [TestCase("4", "3", "1")]
        [TestCase("2BDC8D2", "7", "4")]
        [TestCase("2B", "7", "1")]
        [TestCase("FFFFFFFF", "FFFF", "0")]
        [TestCase("AFAFAFAF", "FFFF", "5F5F")]
        [TestCase("10000000", "FFFF", "1000")]
        [TestCase("FFFFFAAAAFF32549877523487", "3F2782F60D0AD", "3D70A6B5AD60E")]
        [TestCase("8882FABDA2031571278293379", "3F2782F60D0AD", "7328466E5199")]
        [TestCase("11111FFFFF764239156912765", "3F2782F60D0AD", "388C3EF9AE13")]
        public void TestBarrettReduction(string hex1, string hex2, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            ModCalculator modcalculator = new ModCalculator();
            var r = modcalculator.BarrettReduction(a, b);
            Assert.AreEqual(expected, r.ToString());
        }
        /*
        [Test]
        [TestCase("3AE491AB98B294BB8D431D71B1F57478C3014AFB7CFA6FBCF2E0687A4E46B59A",
                  "392D6FABDB9347B4DF92D03F1447B07FE47BB5C0EFD252C36FA5F4897D3A9A6C",
                  "A6DE825B0B63D63B03F54F9A93CB97C1D85E719FA75D1B0C95D110691FF8EC8D",
                  "419289425C18851BAE85DAAE201B54D48FF16994037870A4A62CF735960FD37B")]
        [TestCase("AB9399181470F", "FFAF", "9328F", "460D8")]
        [TestCase("AB9399181470F","0","9328F","1")]
        [TestCase("AB9399181470F", "1", "9328F", "18D9C")]
        public void TestLongModPowerBarrett(string hex1, string hex2, string hex3,string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            var n = new Number(hex3);
            ModCalculator modcalculator = new ModCalculator();
            var r = modcalculator.LongModPowerBarrett(a, b, n);
            Assert.AreEqual(expected, r.ToString());
        }*/
    }
}
