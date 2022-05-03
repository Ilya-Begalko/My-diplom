using System;
using System.Collections.Generic;
using TanksGame.Base.Exceptions;
using TanksGame.Services;

namespace TanksGame.Base
{
    public static class IoC
    {
        private static readonly Dictionary<string, object> Objects = new Dictionary<string, object>();
        private static readonly Dictionary<string, object> Strategies = new Dictionary<string, object>();
        public static Dictionary<string, Action> Acts = new Dictionary<string, Action>();


        static IoC()
        {
            Objects.Add("collisionTrees", new Dictionary<string, Dictionary<int, object>>());
            Objects.Add("collisionTreeBuilder", new CollisionTreeBuilder());
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            Dictionary<string, Func<string, T>> newListStrategies = new Dictionary<string, Func<string, T>>();

            Func<string, T> GetStrategies = (string key) =>
            {
               var strategy = (Func<object[], T>)Strategies[key];
               return strategy.Invoke(args);
            };

            Func<string, T> GetTree = (string key) => (T)Objects[key];

            Func<string, T> DependencyRegistration = (string key) =>
            {
                if (key == "IoC.register" && (string)args[0] == "collisionTree")
                    Strategies["collisionTree"] = args[1];
                return default;
            };

            newListStrategies.Add("IoC.register", DependencyRegistration);
            newListStrategies.Add("collisionTree", GetStrategies);
            newListStrategies.Add("collisionTrees", GetTree);
            newListStrategies.Add("collisionTreeBuilder", GetTree);

            try
            {
                return newListStrategies.TryGetValue(key, out var strategy) ? strategy.Invoke(key) : default;
            }
            catch
            {
                throw new ResolveDependencyException();
            }
        }
    }
}