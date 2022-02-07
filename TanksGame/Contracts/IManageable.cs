using TanksGame.Commands.Abstractions;

namespace TanksGame.Contracts
{
    public interface IManageable
    {
        ICommand GetCommand();
        void SetCommand(ICommand command);
        void RemoveCurrentCommand();
    }
}