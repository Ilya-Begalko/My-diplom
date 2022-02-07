using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.Rotate
{
    public class RotateMacroCommand : ICommand
    {
        private readonly IMoveRotatable _moveRotatable;

        public RotateMacroCommand(IMoveRotatable moveRotatable)
        {
            _moveRotatable = moveRotatable;
        }

        public void Execute()
        {
            new RotateSelfCommand(_moveRotatable).Execute();
            new RotateVelocityCommand(_moveRotatable).Execute();
        }
    }
}