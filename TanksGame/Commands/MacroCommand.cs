using System.Collections.Generic;
using TanksGame.Commands.Abstractions;

namespace TanksGame.Commands
{
    public class MacroCommand : ICommand
    {
        private ICommandQueue _internalCommandQueue;
        private List<ICommand> _commandsToUndo;

        public MacroCommand(ICommandQueue commandQueue, List<ICommand> commands)
        {
            _internalCommandQueue = commandQueue;
            _commandsToUndo = commands;
        }

        public void Execute()
        {
            ICommandQueue tempCommandQueue = _internalCommandQueue.Clone();
            _commandsToUndo.Clear();
            while (_internalCommandQueue.Peek() is not (null, null))
            {
                (ICommand, ICommand) command = _internalCommandQueue.Dequeue();
                command.Item1.Execute();
                _commandsToUndo.Add(command.Item2);
            }

            _internalCommandQueue = tempCommandQueue;
        }
    }
}