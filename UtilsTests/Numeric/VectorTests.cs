using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils.Numeric;

namespace UtilsTests.Numeric
{
    [TestClass]
    public class VectorTests
    {
        private void VectorsShouldBeEquals(Vector first, Vector second)
        {
            Assert.AreEqual(true, Vector.AreEquals(first, second));
        }

        private void VectorsShouldNotBeEquals(Vector first, Vector second)
        {
            Assert.AreEqual(false, Vector.AreEquals(first, second));
        }

        private void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        [TestMethod]
        public void TestAreEqualsTrue()
        {
            var first = new Vector(1, 2, 3);
            var second = new Vector(1, 2, 3);
            VectorsShouldBeEquals(first, second);
        }

        [TestMethod]
        public void TestAreEqualsFalse()
        {
            var first = new Vector(1, 2, 3);
            var second = new Vector(1, 2, 2);
            VectorsShouldNotBeEquals(first, second);
        }

        [TestMethod]
        public void TestSumIsCorrect()
        {
            var first = new Vector(1, 8, 3, -2);
            var second = new Vector(1, 6, -5, 5);
            var third = new Vector(2, 14, -2, 3);
            VectorsShouldBeEquals(third, first + second);
        }

        [TestMethod]
        public void TestSumVectorsNotSameSize()
        {
            var first = new Vector(1, 8, 3, -2);
            var second = new Vector(5, 24, 0);
            ExceptionShouldBeThrew<ArgumentException>(() => Vector.Sum(first, second));
        }
    }
}