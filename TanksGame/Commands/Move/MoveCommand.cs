using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.Move
{
    public class MoveCommand : ICommand
    {
        private readonly IMovable _movable;

        public MoveCommand(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            _movable.Position += _movable.Velocity;
            IoC.Resolve<object>("queue.add", this);
        }
    }
}