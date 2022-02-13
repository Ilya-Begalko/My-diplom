using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;

namespace TanksGame.Commands.Queue
{
    public class CommandQueue : ICommandQueue
    {
        private Queue<(ICommand, ICommand)> _queue = new();

        public CommandQueue()
        {

        }

        private CommandQueue(Queue<(ICommand, ICommand)> queue)
        {
            _queue = queue;
        }

        public void Add(ICommand command)
        {
            _queue.Enqueue((command, IoC.Resolve<ICommand>("EmptyCommand")));
        }

        public void Add((ICommand, ICommand) command)
        {
            _queue.Enqueue(command);
        }

        public (ICommand, ICommand) Dequeue()
        {
            return _queue.Dequeue();
        }

        public (ICommand, ICommand) Peek()
        {
            return _queue.TryPeek(out (ICommand, ICommand) command) ? command : (null, null);
        }

        public ICommandQueue Clone()
        {
            return new CommandQueue(new Queue<(ICommand, ICommand)>(_queue));
        }
    }
}