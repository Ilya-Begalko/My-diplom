using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.Rotate
{
    public class StopRotateCommand : ICommand
    {
        private readonly IMoveRotatable _moveRotatable;

        public StopRotateCommand(IMoveRotatable moveRotatable)
        {
            _moveRotatable = moveRotatable;
        }

        public void Execute()
        {
            IoC.Resolve<object>("queue.remove", _moveRotatable.GetCommand());
            _moveRotatable.RemoveCurrentCommand();
        }
    }
}