using TanksGame.Base;
using TanksGame.Contracts;

namespace TanksGame.Commands.Rotate
{
    public class StartRotateCommand
    {
        private readonly IMoveRotatable _moveRotatable;

        public StartRotateCommand(IMoveRotatable moveRotatable)
        {
            _moveRotatable = moveRotatable;
        }

        public void Execute()
        {
            var rotateMacroCommand = new RotateMacroCommand(_moveRotatable);
            IoC.Resolve<object>("queue.add", rotateMacroCommand);
            _moveRotatable.SetCommand(rotateMacroCommand);
        }
    }
}