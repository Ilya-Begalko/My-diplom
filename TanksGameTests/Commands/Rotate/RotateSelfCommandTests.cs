using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Commands.Rotate;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGameTests.Commands.Move
{
    [TestClass]
    public class RotateSelfCommandTests
    {
        private void LetObjectHasDirection(Mock<IRotatable> mock, Vector vector)
        {
            mock.SetupGet(m => m.Direction).Returns(vector).Verifiable();
        }

        private void LetObjectHasAngularVelocity(Mock<IRotatable> mock, int angularVelocity)
        {
            mock.SetupGet(m => m.AngularVelocity).Returns(angularVelocity).Verifiable();
        }

        private void ThenObjectMustBeRotatedToDirection(Mock<IRotatable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Direction = It.Is<Vector>(v => Vector.AreEquals(v, vector))).Verifiable();
        }

        private void LetThrowsExceptionIfCantGetDirection(Mock<IRotatable> mock)
        {
            mock.SetupGet(m => m.Direction).Throws<Exception>();
        }

        private void LetThrowsExceptionIfCantSetDirection(Mock<IRotatable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Direction = It.IsAny<Vector>()).Throws<Exception>();
        }

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        private static void AllActionsWithMockShouldBePerformed(Mock<IRotatable> mock)
        {
            mock.VerifyAll();
        }

        [TestMethod]
        public void RotateCommandShouldRotateObject()
        {
            var mock = new Mock<IRotatable>();
            LetObjectHasDirection(mock, new Vector(3, 8));
            LetObjectHasAngularVelocity(mock, 180);
            ThenObjectMustBeRotatedToDirection(mock, new Vector(-3, -8));
            new RotateSelfCommand(mock.Object).Execute();
            AllActionsWithMockShouldBePerformed(mock);
        }

        [TestMethod]
        public void RotateCommandExceptionIfCantGetDirection()
        {
            var mock = new Mock<IRotatable>();
            LetThrowsExceptionIfCantGetDirection(mock);
            ExceptionShouldBeThrew<Exception>(() => new RotateSelfCommand(mock.Object).Execute());
        }

        [TestMethod]
        public void RotateCommandExceptionIfCantSetDirection()
        {
            var mock = new Mock<IRotatable>();
            LetObjectHasDirection(mock, new Vector(3, 8));
            LetObjectHasAngularVelocity(mock, 9);
            LetThrowsExceptionIfCantSetDirection(mock, new Vector(3, 4));
            ExceptionShouldBeThrew<Exception>(() => new RotateSelfCommand(mock.Object).Execute());
        }
    }
}