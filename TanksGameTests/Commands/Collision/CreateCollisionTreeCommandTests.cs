using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TanksGame.Base;
using TanksGame.Commands.Collision;

namespace TanksGameTests.Commands.Collision
{
    [TestClass]
    public class CreateCollisionTreeCommandTests
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

        private static void LetsCreateATreeFromFile(string path)
        {
            new CreateCollisionTreeCommand(path).Execute();
        }

        private static Dictionary<string, Dictionary<int, object>> LetsGetAllTrees()
        {
            return IoC.Resolve<Dictionary<string, Dictionary<int, object>>>("collisionTrees");
        }

        private static void CountOfAllTreesMustBeN(Dictionary<string, Dictionary<int, object>> trees, int n)
        {
            Assert.AreEqual(n, trees.Count);
        }

        private static void CountOfThisTreeMustBeN(string treeName, int n)
        {
            Assert.AreEqual(n, IoC.Resolve<Dictionary<int, object>>("collisionTree", treeName).Count);
        }

        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        [TestMethod]
        public void CreateCollisionTreeCommandShouldCreateATree()
        {
            LetsRegisterAllDependecties();
            LetsCreateATreeFromFile("./Data/CollisionTreesData/lt_lt.txt");
            var collisionTrees = LetsGetAllTrees();
            CountOfAllTreesMustBeN(collisionTrees, 1);
            CountOfThisTreeMustBeN("lt_lt", 4);
        }

        [TestMethod]
        public void CreateCollisionTreeCommandExceptionIfCanNotReadFile()
        {
            LetsRegisterAllDependecties();
            ExceptionShouldBeThrew<FileNotFoundException>(() =>
                LetsCreateATreeFromFile("./Data/CollisionTreesData/lt_mt.txt"));
        }
    }
}