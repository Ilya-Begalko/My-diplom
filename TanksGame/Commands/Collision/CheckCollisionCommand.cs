using System;
using System.Collections.Generic;
using System.Linq;
using TanksGame.Adapters;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;

namespace TanksGame.Commands.Collision
{
    /// <summary>
    ///     Команда, которая проверяет факт столкновения двух объектов и генерирует событие
    ///     в случае столкновения
    /// </summary>
    public class CheckCollisionCommand : IEmitCommand
    {
        private readonly IGameObject _firstMovableObject;
        private readonly IGameObject _secondMovableObject;

        public CheckCollisionCommand(IGameObject firstMovableObject, IGameObject secondMovableObject,
            Action onCollision)
        {
            _firstMovableObject = firstMovableObject;
            _secondMovableObject = secondMovableObject;
            Event += onCollision;
        }

        public event Action Event;

        public void Execute()
        {
            var features = GetFeatures();
            CheckCollision(features);
        }

        private void CheckCollision(IEnumerable<int> features)
        {
            var treeName = $"{_firstMovableObject.GetProperty("Type")}_{_secondMovableObject.GetProperty("Type")}";

            var tree = IoC.Resolve<Dictionary<int, object>>("collisionTree", treeName);
            var subTree = tree;
            foreach (var feature in features)
            {
                subTree = (Dictionary<int, object>)subTree[feature];
            }

            var isCollision = subTree.Keys.First() == 1;
            if (isCollision)
            {
                Event?.Invoke();
            }
        }

        private IEnumerable<int> GetFeatures()
        {
            var deltaPositionX = new MovableAdapter(_firstMovableObject).Position.GetNComponent(1) -
                                 new MovableAdapter(_secondMovableObject).Position.GetNComponent(1);
            var deltaPositionY = new MovableAdapter(_firstMovableObject).Position.GetNComponent(2) -
                                 new MovableAdapter(_secondMovableObject).Position.GetNComponent(2);
            var deltaVelocityX = new MovableAdapter(_firstMovableObject).Velocity.GetNComponent(1) -
                                 new MovableAdapter(_secondMovableObject).Velocity.GetNComponent(1);
            var deltaVelocityY = new MovableAdapter(_firstMovableObject).Velocity.GetNComponent(2) -
                                 new MovableAdapter(_secondMovableObject).Velocity.GetNComponent(2);

            return new List<int> { deltaPositionX, deltaPositionY, deltaVelocityX, deltaVelocityY };
        }
    }
}