using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGame.Adapters
{
    public class MovableAdapter : IMovable
    {
        private readonly IGameObject _instance;

        public MovableAdapter(IGameObject instance)
        {
            _instance = instance;
        }

        public Vector Position
        {
            get => (Vector)_instance.GetProperty("Position");
            set => _instance.SetProperty("Position", value);
        }

        public Vector Velocity
        {
            get => (Vector)_instance.GetProperty("Velocity");
            set => _instance.SetProperty("Velocity", value);
        }

        public ICommand GetCommand()
        {
            return (ICommand)_instance.GetProperty("moveCommand");
        }

        public void RemoveCurrentCommand()
        {
            _instance.RemoveProperty("moveCommand");
        }

        public void SetCommand(ICommand command)
        {
            _instance.SetProperty("moveCommand", command);
        }
    }
}