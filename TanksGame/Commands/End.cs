using System.Collections.Generic;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using TanksGame.Base.Exceptions;

namespace TanksGame.Commands
{
    public class End : ICommand
    {
        private readonly IStopable _stopable;
        private readonly ICommand _empty;
        public End(IStopable stopable, ICommand empty)
        {
            _stopable = stopable;
            _empty = empty;
        }


        public void Execute()
        {
            _stopable.Repeat = _empty;
        }
    }
}