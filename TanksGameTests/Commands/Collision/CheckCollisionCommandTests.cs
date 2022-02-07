using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Commands.Collision;
using Utils.Numeric;

namespace TanksGameTests.Commands.Collision
{
    [TestClass]
    public class CheckCollisionCommandTests
    {
        private static void LetsRegisterAllDependecties()
        {
            IoC.Resolve<Dictionary<int, HashSet<int>>>("IoC.register", "collisionTree",
                new Func<object[], Dictionary<int, object>>(args =>
                    {
                        var treeName = (string)args[0];
                        var tree = new Dictionary<int, object>();
                        var collisionTrees = IoC.Resolve<Dictionary<string, Dictionary<int, object>>>("collisionTrees");
                        if (!collisionTrees.ContainsKey(treeName))
                        {
                            collisionTrees[treeName] = tree;
                        }

                        return collisionTrees[treeName];
                    }
                )
            );
        }

        private static void LetObjectHasPosition(Mock<IGameObject> mock, Vector vector)
        {
            mock.Setup(m => m.GetProperty("Position")).Returns(vector).Verifiable();
        }

        private void LetThrowsExceptionIfCantGetPostition(Mock<IGameObject> mock)
        {
            mock.Setup(m => m.GetProperty("Position")).Throws<Exception>();
        }

        private static void LetObjectHasVelocity(Mock<IGameObject> mock, Vector vector)
        {
            mock.Setup(m => m.GetProperty("Velocity")).Returns(vector).Verifiable();
        }

        private void LetThrowsExceptionIfCantGetVelocity(Mock<IGameObject> mock)
        {
            mock.Setup(m => m.GetProperty("Velocity")).Throws<Exception>();
        }

        private static void LetObjectHasType(Mock<IGameObject> mock, string type)
        {
            mock.Setup(m => m.GetProperty("Type")).Returns(type).Verifiable();
        }

        private void LetThrowsExceptionIfCantGetType(Mock<IGameObject> mock)
        {
            mock.Setup(m => m.GetProperty("Type")).Throws<Exception>();
        }

        private static void LetsCreateATreeFromFile(string path)
        {
            new CreateCollisionTreeCommand(path).Execute();
        }

        private static void LetsDoItOnCollision(IEmitCommand command, Action action)
        {
            command.Event += action;
        }

        private static void ItMustBeCollision(int countOfRaisedEvents)
        {
            Assert.AreEqual(1, countOfRaisedEvents);
        }

        private static void ItMustNotBeCollision(int countOfRaisedEvents)
        {
            Assert.AreEqual(0, countOfRaisedEvents);
        }

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        [TestMethod]
        public void CollisionCommandShouldDefineCollision()
        {
            var countOfRaisedEvents = 0;
            LetsRegisterAllDependecties();
            LetsCreateATreeFromFile("./Data/CollisionTreesData/lt_lt.txt");
            var mock1 = new Mock<IGameObject>();
            LetObjectHasPosition(mock1, new Vector(3, 8));
            LetObjectHasVelocity(mock1, new Vector(2, -3));
            LetObjectHasType(mock1, "lt");
            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(3, 8));
            LetObjectHasVelocity(mock2, new Vector(2, -3));
            LetObjectHasType(mock2, "lt");

            var checkCollisionCommand = new CheckCollisionCommand(mock1.Object, mock2.Object, () => { });
            LetsDoItOnCollision(checkCollisionCommand, () => { countOfRaisedEvents++; });
            checkCollisionCommand.Execute();
            ItMustBeCollision(countOfRaisedEvents);
        }

        [TestMethod]
        public void CollisionCommandShouldDefineNoCollision()
        {
            var countOfRaisedEvents = 0;
            LetsRegisterAllDependecties();
            LetsCreateATreeFromFile("./Data/CollisionTreesData/lt_lt.txt");
            var mock1 = new Mock<IGameObject>();
            LetObjectHasPosition(mock1, new Vector(3, 8));
            LetObjectHasVelocity(mock1, new Vector(2, -3));
            LetObjectHasType(mock1, "lt");
            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(-2, 3));
            LetObjectHasVelocity(mock2, new Vector(-2, -5));
            LetObjectHasType(mock2, "lt");

            var checkCollisionCommand = new CheckCollisionCommand(mock1.Object, mock2.Object, () => { });
            LetsDoItOnCollision(checkCollisionCommand, () => { countOfRaisedEvents++; });
            checkCollisionCommand.Execute();
            ItMustNotBeCollision(countOfRaisedEvents);
        }

        [TestMethod]
        public void CollisionCommandExceptionIfNotEnoughData()
        {
            LetsRegisterAllDependecties();
            LetsCreateATreeFromFile("./Data/CollisionTreesData/lt_lt.txt");
            var mock1 = new Mock<IGameObject>();
            LetObjectHasPosition(mock1, new Vector(3, 8));
            LetObjectHasVelocity(mock1, new Vector(2, -3));
            LetObjectHasType(mock1, "lt");
            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(-2, 3));
            LetObjectHasVelocity(mock2, new Vector(-9, 0));
            LetObjectHasType(mock2, "lt");

            ExceptionShouldBeThrew<KeyNotFoundException>(() =>
            {
                new CheckCollisionCommand(mock1.Object, mock2.Object, () => { }).Execute();
            });
        }

        [TestMethod]
        public void CreateCollisionTreeCommandExceptionIfCantGetPosition()
        {
            var mock1 = new Mock<IGameObject>();
            LetThrowsExceptionIfCantGetPostition(mock1);
            LetObjectHasVelocity(mock1, new Vector(2, 9));
            LetObjectHasType(mock1, "lt");

            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(-1, 8));
            LetObjectHasVelocity(mock2, new Vector(7, 0));
            LetObjectHasType(mock2, "lt");

            ExceptionShouldBeThrew<Exception>(() =>
                new CheckCollisionCommand(mock1.Object, mock2.Object, () => { }).Execute());
        }

        [TestMethod]
        public void CreateCollisionTreeCommandExceptionIfCantGetVelocity()
        {
            var mock1 = new Mock<IGameObject>();
            LetObjectHasPosition(mock1, new Vector(2, 5));
            LetObjectHasVelocity(mock1, new Vector(0, 9));
            LetObjectHasType(mock1, "lt");

            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(4, 1));
            LetThrowsExceptionIfCantGetVelocity(mock2);
            LetObjectHasType(mock2, "lt");

            ExceptionShouldBeThrew<Exception>(() =>
                new CheckCollisionCommand(mock1.Object, mock2.Object, () => { }).Execute());
        }

        [TestMethod]
        public void CreateCollisionTreeCommandExceptionIfCantGetType()
        {
            var mock1 = new Mock<IGameObject>();
            LetObjectHasPosition(mock1, new Vector(1, -5));
            LetObjectHasVelocity(mock1, new Vector(0, -9));
            LetObjectHasType(mock1, "lt");

            var mock2 = new Mock<IGameObject>();
            LetObjectHasPosition(mock2, new Vector(4, 1));
            LetObjectHasVelocity(mock2, new Vector(0, 0));
            LetThrowsExceptionIfCantGetType(mock2);

            ExceptionShouldBeThrew<Exception>(() =>
                new CheckCollisionCommand(mock1.Object, mock2.Object, () => { }).Execute());
        }
    }
}