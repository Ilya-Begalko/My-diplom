using System;

namespace TanksGame.Commands.Abstractions
{
    /// <summary>
    ///     Абстрактная команда с обработкой событий
    /// </summary>
    public interface IEmitCommand : ICommand
    {
        event Action Event;
    }
}