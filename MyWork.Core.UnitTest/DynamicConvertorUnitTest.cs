using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MyWork.Core.UnitTest
{
    [TestClass]
    public class DynamicConvertorUnitTest
    {
        [TestMethod]
        public void TestConvertInt()
        {
            var typename = typeof(int).FullName;
            var expected = 10;
            var actural = DynamicConvertor.To("10", typename);
            Assert.AreEqual(expected, actural);
        }

        [TestMethod]
        public void TestConvertDate()
        {
            var typename = typeof(DateTime).FullName;
            var expected = DateTime.ParseExact("2022-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var actural = DynamicConvertor.To("2022-01-01", typename);
            Assert.AreEqual(expected, actural);
        }
        [TestMethod]
        public void TestConvertBoolean()
        {
            var typename = typeof(bool).FullName;
            var expected =true;
            var actural = DynamicConvertor.To("true", typename);
            Assert.AreEqual(expected, actural);
        }
    }
}
