using System.Collections.Generic;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using TanksGame.Base.Exceptions;

namespace TanksGame.Commands
{
    public class Repeat : ICommand
    {
        private readonly IStopable _stopable;
        private readonly ICommand _command;
        private readonly ICommandQueue _queue;

        public Repeat(IStopable stopable, ICommandQueue queue, ICommand command)
        {
            _stopable = stopable;
            _queue = queue;
            _command = command;
        }

        public void Execute()
        {
            _command.Execute();
            _queue.Add((_stopable.Repeat, IoC.Resolve<ICommand>("EmptyCommand")));
        }
    }
}