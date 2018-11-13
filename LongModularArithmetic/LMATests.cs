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
    }
}
