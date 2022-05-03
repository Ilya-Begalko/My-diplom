using System;

namespace TanksGame.Commands.Abstractions
{
    public interface IEmitCommand : ICommand
    {
        event Action Event;
    }
}