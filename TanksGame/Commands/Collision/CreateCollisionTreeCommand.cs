using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Services.Abstractions;

namespace TanksGame.Commands.Collision
{
    /// <summary>
    ///     Команда, которая создает на основе файла с данными о наличии / отсутствии столкновений
    ///     дерево принятия решений
    /// </summary>
    public class CreateCollisionTreeCommand : ICommand
    {
        private readonly string _dataPath;

        public CreateCollisionTreeCommand(string dataPath)
        {
            _dataPath = dataPath;
        }

        public void Execute()
        {
            IoC.Resolve<ICollisionTreeBuilder>("collisionTreeBuilder").FromFile(_dataPath);
        }
    }
}