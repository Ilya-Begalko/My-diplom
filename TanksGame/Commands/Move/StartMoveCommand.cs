using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.Move
{
    public class StartMoveCommand : ICommand
    {
        private readonly IMovable _movable;

        public StartMoveCommand(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            var moveCommand = new MoveCommand(_movable);
            IoC.Resolve<object>("queue.add", moveCommand);
            _movable.SetCommand(moveCommand);
        }
    }
}