using System.Collections.Generic;

namespace TanksGame.Commands.Abstractions
{
    public interface ICommandQueue
    {
        public void Add((ICommand, ICommand) command);
        public (ICommand, ICommand) Dequeue();
        public (ICommand, ICommand) Peek();
        public ICommandQueue Clone();
    }
}