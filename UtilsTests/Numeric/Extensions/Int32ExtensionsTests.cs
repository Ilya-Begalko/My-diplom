using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Numeric.Extensions;

namespace UtilsTests.Numeric.Extensions
{
    [TestClass]
    public class Int32ExtensionsTests
    {
        [TestMethod]
        public void ToRadiansTest()
        {
            Assert.IsTrue(0.15708.AreTrueEquals(9.ToRadians(), 1e-5));
            Assert.IsTrue(Math.PI.AreTrueEquals(180.ToRadians(), 1e-5));
            Assert.IsTrue(0.0.AreTrueEquals(0.ToRadians(), 1e-5));
        }
    }
}