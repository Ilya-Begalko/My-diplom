using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.Move
{
    public class StopMoveCommand : ICommand
    {
        private readonly IMovable _movable;

        public StopMoveCommand(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            IoC.Resolve<object>("queue.remove", _movable.GetCommand());
            _movable.RemoveCurrentCommand();
        }
    }
}