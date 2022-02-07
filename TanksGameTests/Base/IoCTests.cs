using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TanksGame.Base;
using TanksGame.Base.Exceptions;

namespace TanksGameTests.Base
{
    [TestClass]
    public class IoCTests
    {
        private static void ExceptionShouldBeThrew<T>(Action action) where T : Exception
        {
            Assert.ThrowsException<T>(action);
        }

        [TestMethod]
        public void IocRegistrateExceptionIfNoKey()
        {
            ExceptionShouldBeThrew<ResolveDependencyException>(() =>
            {
                IoC.Resolve<object>("IoC.register", new Func<object, object>(o => null));
            });
        }

        [TestMethod]
        public void IocRegistrateExceptionIfNoParams()
        {
            ExceptionShouldBeThrew<ResolveDependencyException>(() => { IoC.Resolve<object>("IoC.register"); });
        }
    }
}