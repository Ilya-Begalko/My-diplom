using TanksGame.Commands.Abstractions;

namespace TanksGame.Contracts
{
    public interface IStopable
    {
        ICommand Repeat { get; set; }
    }
}