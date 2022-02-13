using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Commands;
using TanksGame.Commands.Abstractions;
using TanksGame.Commands.Move;
using TanksGame.Commands.Rotate;
using TanksGame.Commands.Queue;
using TanksGame.Commands.HardReset;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGameTests.Commands.HardReset
{
    [TestClass]
    public class HardResetCommandTests
    {

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        [TestMethod]
        public void HardResetTest()
        {
            var mockMovable = new Mock<IMovable>();
            var initPosition = new Vector(3, 8);
            var position = new Vector(5, 5);
            var velocityMovable = new Vector(2, -3);
            mockMovable.SetupSet(x => x.Position = It.IsAny<Vector>()).Callback<Vector>(x => position = x);
            mockMovable.SetupGet(x => x.Position).Returns(() => position);
            mockMovable.SetupGet(x => x.Velocity).Returns(() => velocityMovable);
            IMovable movable = mockMovable.Object;
            movable.Position = new Vector(3, 8);
            ICommand move = new MoveCommand(movable);

            CommandQueue commandQueue = new CommandQueue();
            commandQueue.Add(move);
            commandQueue.Add(move);
            commandQueue.Add(new HardResetCommand(commandQueue));
            commandQueue.Add(move);
            commandQueue.Add(move);
            MacroCommand macroCommand = new MacroCommand(commandQueue, new List<ICommand>());

            macroCommand.Execute();

            ExceptionShouldBeThrew<InvalidOperationException>(() => macroCommand.Execute());
        }
    }
}