using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Commands.Move;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGameTests.Commands.Move
{
    [TestClass]
    public class MoveCommandTests
    {
        private static void LetObjectHasPosition(Mock<IMovable> mock, Vector vector)
        {
            mock.SetupGet(m => m.Position).Returns(vector).Verifiable();
        }

        private static void LetObjectHasVelocity(Mock<IMovable> mock, Vector vector)
        {
            mock.SetupGet(m => m.Velocity).Returns(vector).Verifiable();
        }

        private static void ThenObjectMustBeMovedToPosition(Mock<IMovable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Position = It.Is<Vector>(v => Vector.AreEquals(v, vector))).Verifiable();
        }

        private static void LetThrowsExceptionIfCantGetPosition(Mock<IMovable> mock)
        {
            mock.SetupGet(m => m.Position).Throws<Exception>();
        }

        private static void LetThrowsExceptionIfCantGetVelocity(Mock<IMovable> mock)
        {
            mock.SetupGet(m => m.Velocity).Throws<Exception>();
        }

        private static void LetThrowsExceptionIfCantSetPosition(Mock<IMovable> mock, Vector vector)
        {
            mock.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<Exception>();
        }

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        private static void AllActionsWithMockShouldBePerformed(Mock<IMovable> mock)
        {
            mock.VerifyAll();
        }

        [TestMethod]
        public void MoveCommandShouldMoveObject()
        {
            var mock = new Mock<IMovable>();
            LetObjectHasPosition(mock, new Vector(3, 8));
            LetObjectHasVelocity(mock, new Vector(2, -3));
            ThenObjectMustBeMovedToPosition(mock, new Vector(5, 5));
            new MoveCommand(mock.Object).Execute();
            AllActionsWithMockShouldBePerformed(mock);
        }

        [TestMethod]
        public void MoveCommandExceptionIfCantGetPosition()
        {
            var mock = new Mock<IMovable>();
            LetThrowsExceptionIfCantGetPosition(mock);
            ExceptionShouldBeThrew<Exception>(() => new MoveCommand(mock.Object).Execute());
        }

        [TestMethod]
        public void MoveCommandExceptionIfCantGetVelocity()
        {
            var mock = new Mock<IMovable>();
            LetThrowsExceptionIfCantGetVelocity(mock);
            ExceptionShouldBeThrew<Exception>(() => new MoveCommand(mock.Object).Execute());
        }

        [TestMethod]
        public void MoveCommandExceptionIfCantSetPosition()
        {
            var mock = new Mock<IMovable>();
            LetObjectHasPosition(mock, new Vector(3, 8));
            LetObjectHasVelocity(mock, new Vector(2, -3));
            LetThrowsExceptionIfCantSetPosition(mock, new Vector(3, 4));
            ExceptionShouldBeThrew<Exception>(() => new MoveCommand(mock.Object).Execute());
        }
    }
}