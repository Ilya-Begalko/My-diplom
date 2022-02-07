using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Commands.Rotate;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGameTests.Commands.Rotate
{
    [TestClass]
    public class RotateVelocityCommandTests
    {
        private void LetObjectHasVelocity(Mock<IMoveRotatable> mock, Vector vector)
        {
            mock.SetupGet(m => m.Velocity).Returns(vector).Verifiable();
        }

        private void LetObjectHasAngularVelocity(Mock<IMoveRotatable> mock, int angularVelocity)
        {
            mock.SetupGet(m => m.AngularVelocity).Returns(angularVelocity).Verifiable();
        }

        private void ThenVelocityMustBeRotatedToDirection(Mock<IMoveRotatable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Velocity = It.Is<Vector>(v => Vector.AreEquals(v, vector))).Verifiable();
        }

        private void LetThrowsExceptionIfCantGetVelocity(Mock<IMoveRotatable> mock)
        {
            mock.SetupGet(m => m.Velocity).Throws<Exception>();
        }

        private void LetThrowsExceptionIfCantSetVelocity(Mock<IMoveRotatable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Velocity = It.IsAny<Vector>()).Throws<Exception>();
        }

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        private static void AllActionsWithMockShouldBePerformed(Mock<IMoveRotatable> mock)
        {
            mock.VerifyAll();
        }

        [TestMethod]
        public void RotateCommandShouldRotateObject()
        {
            var mock = new Mock<IMoveRotatable>();
            LetObjectHasVelocity(mock, new Vector(5, 4));
            LetObjectHasAngularVelocity(mock, 180);
            ThenVelocityMustBeRotatedToDirection(mock, new Vector(-5, -4));
            new RotateVelocityCommand(mock.Object).Execute();
            AllActionsWithMockShouldBePerformed(mock);
        }

        [TestMethod]
        public void RotateCommandExceptionIfCantGetVelocity()
        {
            var mock = new Mock<IMoveRotatable>();
            LetThrowsExceptionIfCantGetVelocity(mock);
            ExceptionShouldBeThrew<Exception>(() => new RotateVelocityCommand(mock.Object).Execute());
        }

        [TestMethod]
        public void RotateCommandExceptionIfCantSetVelocity()
        {
            var mock = new Mock<IMoveRotatable>();
            LetObjectHasVelocity(mock, new Vector(3, 8));
            LetObjectHasAngularVelocity(mock, 9);
            LetThrowsExceptionIfCantSetVelocity(mock, new Vector(3, 4));
            ExceptionShouldBeThrew<Exception>(() => new RotateVelocityCommand(mock.Object).Execute());
        }
    }
}