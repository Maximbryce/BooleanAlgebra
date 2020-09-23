using NUnit.Framework;
using NumberConversion;

namespace NumberConversionTests
{
    public class Tests
    {
        [Test]
        public void decConversion()
        {
            // Using the value 23 which is 10111, 17
            //Simple conversion tests
            Assert.AreEqual("23", NumberConvert.ConvertToBase("10111", 2, 10));
            Assert.AreEqual("23", NumberConvert.ConvertToBase("17", 16, 10));
        }

        [Test]
        public void hexConversion()
        {
            Assert.AreEqual("17", NumberConvert.ConvertToBase("10111", 2, 16, 2));
            Assert.AreEqual("17", NumberConvert.ConvertToBase("23", 10, 16, 2));
        }

        [Test]
        public void binConversion()
        {
            Assert.AreEqual("10111", NumberConvert.ConvertToBase("23", 10, 2, 5));
            Assert.AreEqual("10111", NumberConvert.ConvertToBase("17", 16, 2, 5));
        }

        [Test]
        public void hexLength()
        {
            Assert.AreNotEqual("17", NumberConvert.ConvertToBase("10111", 2, 16, 10));
            Assert.AreEqual("0017", NumberConvert.ConvertToBase("23", 10, 16, 4));
            Assert.AreEqual("17", NumberConvert.ConvertToBase("10111", 2, 16, 1));
        }

        [Test]
        public void binLength()
        {
            Assert.AreNotEqual("10111", NumberConvert.ConvertToBase("17", 16, 2, 10));
            Assert.AreEqual("0000010111", NumberConvert.ConvertToBase("17", 16, 2, 10));
            Assert.AreEqual("10111", NumberConvert.ConvertToBase("17", 16, 2, 4));
        }

        [Test]
        public void twoComplimentTest()
        {
            Assert.AreEqual("111111", NumberConvert.TwosComplement("000001", 2));
            Assert.AreEqual("1111111011", NumberConvert.TwosComplement("000101", 2, 10));
        }

    }
}