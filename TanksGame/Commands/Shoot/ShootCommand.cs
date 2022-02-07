using TanksGame.Adapters;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Commands.Move;
using Utils.Numeric;

namespace TanksGame.Commands.Shoot
{
    public class ShootCommand : ICommand
    {
        public void Execute()
        {
            var projectile = IoC.Resolve<IGameObject>("projectile", new Vector(5, 3), 200);
            new MoveCommand(new MovableAdapter(projectile)).Execute();
        }
    }
}