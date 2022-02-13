using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Commands;
using TanksGame.Commands.Abstractions;
using TanksGame.Commands.Move;
using TanksGame.Commands.Queue;
using TanksGame.Commands.Empty;

using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGameTests.Commands.Queue
{
    [TestClass]
    public class CommandQueueTests
    {
        [TestMethod]
        public void StartQueueTest()
        {
            var mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(x => x.Velocity).Returns(It.IsAny<Vector>());
            var mockStopable = new Mock<IStopable>();
            ICommand move = new MoveCommand(mockMovable.Object);
            ICommandQueue queue = new CommandQueue();
            ICommand startMove = new Repeat(mockStopable.Object, queue, move);
            mockStopable.SetupGet(x => x.Repeat).Returns(() => startMove);

            ICommand empty = new EmptyCommand();

            Assert.AreNotEqual(startMove, empty);
        }

        [TestMethod]
        public void EndQueueTest()
        {
            var mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.Execute());
            
            ICommandQueue queue = new CommandQueue();
            
            var mockStopable = new Mock<IStopable>();
            
            ICommand startMove = new Repeat(mockStopable.Object, queue, mockCommand.Object);
            ICommand command = startMove;
            
            mockStopable.SetupSet(x => x.Repeat = It.IsAny<ICommand>()).Callback<ICommand>(x => command = x);
            mockStopable.SetupGet(x => x.Repeat).Returns(() => command);

            ICommand empty = new EmptyCommand();
            
            ICommand endMove = new End(mockStopable.Object, empty);
            
            startMove.Execute();
            endMove.Execute();
            queue.Dequeue().Item1.Execute();
            ICommand actual = queue.Peek().Item1;

            Assert.AreEqual(empty, actual);
        }
    }
 }