﻿using System;
using NUnit.Framework;
using LongModArithmetic;

namespace LongModArithmetics.Tests
{
    [TestFixture]
    public class ModCalculatorTests
    {
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
        [TestCase("E3AE74A8EC74A313C8BED20D4349D9EFBA356FE6E8AD89E21C2D028111C06F422DC8E0F2E5F448830D1D61B5A7AEAC088E31AA290891E06AD83C0786443611AF",
                  "49FE1165CB7A21D12D6ACEC225285544B36BABD3F8B4DD8D94EDE1A444B9738ADF06CDB40DCAFA87B25A8BECA2D2262A53D8431A119405F0CBEFB83D2AD547CC",
                  "5B440778A063DA0407E65C6D3D0DA219FF26C6AFE8EF1395D635D94439414A190B477D6BC9358EBF60DBDEFBF38398992A8E0DAD3D5CE98746CDECEC3B63A4B")]
        public void TestBarrettReduction(string hex1, string hex2, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            ModCalculator modcalculator = new ModCalculator();
            Calculator calculator = new Calculator();
            Number m = modcalculator.MuConstant(a, b);
            int k = calculator.BitLength(a);
            var r = modcalculator.BarrettReduction(a, b);
            Assert.AreEqual(expected, r.ToString());
        }



        [Test]
        [TestCase("AB9399181470F", "FFAF", "9328F", "460D8")]
        [TestCase("AB9399181470F","0","9328F","1")]
        [TestCase("AB9399181470F", "1", "9328F", "18D9C")]
        [TestCase("FFFFFFFFFFFFFFFFFFFFFFFFFF", "FFFFFFFFFFF", "FF", "0")]

        public void TestLongModPowerBarrett(string hex1, string hex2, string hex3, string expected)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            var n = new Number(hex3);
            ModCalculator modcalculator = new ModCalculator();
            var r = modcalculator.LongModPowerBarrett(a, b, n);
            Assert.AreEqual(expected, r.ToString());
        }
    }
}
