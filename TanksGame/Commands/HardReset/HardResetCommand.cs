using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;

namespace TanksGame.Commands.HardReset
{

    public class HardResetCommand : ICommand
    {
        private ICommandQueue _commandQueue;

        public HardResetCommand(ICommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public void Execute()
        {
            _commandQueue.Dequeue();
            IoC.Resolve<object>("queue.add", this);
        }
    }
}