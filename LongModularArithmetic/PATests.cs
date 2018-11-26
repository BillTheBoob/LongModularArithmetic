using NUnit.Framework;
using LongModularArithmetic;

namespace PrimalityAlgorithms.Tests
{
    [TestFixture]
    public class PATests
    {
        [Test]
        [Description("Verifies that JacobiSymbol function works correctly.")]
        [TestCase("9", 1, "B", "1", 1)]
        [TestCase("D", 1, "F", "1", -1)]
        [TestCase("3E9", 1, "26B3", "1", -1)]
        [TestCase("5", 1, "15", "1", 1)]
        [TestCase("1E", 1, "39", "0", 1)]
        [TestCase("22", -1, "2695", "0", 1)]
        [TestCase("2293465C7", 1, "98723415132473", "1", -1)]
        [TestCase("81885C8050E26BD95368AB5CA79C38FC8F64DDCB48E1C4BE5C9A360A86B5FE5E", 1, "F1FB52AEDDC2396DB3493E3BE65AB871DD8D21B8653221797185EED3A7DAFD91", "1", -1 )]
        public void TestJacobiSymbol(string hex1, int signA,string hex2, string expected, int signRes)
        {
            var a = new Number(hex1);
            var b = new Number(hex2);
            a.sign = signA;
            PrimalityAlgorithm primality = new PrimalityAlgorithm();
            var result = primality.JacobiSymbol(a, b);
            Assert.AreEqual(expected, result.ToString());
            Assert.AreEqual(signRes, result.sign);
        }


        [Test]
        [Description("Verifies that Solovay–Strassen primality test works correctly.")]
        [TestCase("133", 2000, true)]
        public void TestSolovayStrassen(string hex, int k, bool expected)
        {
            var n = new Number(hex);
            PrimalityAlgorithm primality = new PrimalityAlgorithm();
            bool result = primality.SolovayStrassenPrimalityTest(n, k);
            Assert.AreEqual(expected, result);     
        }
    }
}
