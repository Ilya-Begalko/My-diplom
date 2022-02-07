using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGame.Adapters
{
    internal class MoveRotatableAdapter : IMoveRotatable
    {
        private readonly IGameObject _instance;

        public MoveRotatableAdapter(IGameObject instance)
        {
            _instance = instance;
        }

        public Vector Direction
        {
            get => (Vector)_instance.GetProperty("Direction");
            set => _instance.SetProperty("Direction", value);
        }

        public int AngularVelocity
        {
            get => (int)_instance.GetProperty("AngularVelocity");
            set => _instance.SetProperty("AngularVelocity", value);
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
            return (ICommand)_instance.GetProperty("moveRotateCommand");
        }

        public void RemoveCurrentCommand()
        {
            _instance.RemoveProperty("moveRotateCommand");
        }

        public void SetCommand(ICommand command)
        {
            _instance.SetProperty("moveRotateCommand", command);
        }
    }
}