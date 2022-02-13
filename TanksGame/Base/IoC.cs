using System;
using System.Collections.Generic;
using TanksGame.Base.Exceptions;
using TanksGame.Services;

namespace TanksGame.Base
{
    public static class IoC
    {
        public delegate object Creator(params object[] args);
        private static readonly Dictionary<string, object> Objects = new Dictionary<string, object>();
        private static readonly Dictionary<string, object> Strategies = new Dictionary<string, object>();

        static IoC()
        {
            Objects.Add("collisionTrees", new Dictionary<string, Dictionary<int, object>>());
            Objects.Add("collisionTreeBuilder", new CollisionTreeBuilder());
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            try
            {
                // секция регистрации
                if (key == "IoC.register")
                {
                    if ((string)args[0] == "collisionTree")
                    {
                        Strategies["collisionTree"] = args[1];
                    }

                    // TODO регистрация других зависимостей
                }

                // секция разрешения
                else if (key == "collisionTree")
                {
                    var strategy = (Func<object[], T>)Strategies[key];
                    return strategy.Invoke(args);
                }

                else if (key == "collisionTrees")
                {
                    return (T)Objects[key];
                }

                else if (key == "collisionTreeBuilder")
                {
                    return (T) Objects[key];
                }

                return default;
            }
            catch
            {
                throw new ResolveDependencyException();
            }
        }
    }
}