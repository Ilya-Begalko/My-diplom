using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using Utils.Numeric;

namespace TanksGame.Adapters
{
    public class RotatableAdapter : IRotatable
    {
        private readonly IGameObject _instance;

        public RotatableAdapter(IGameObject instance)
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

        public ICommand GetCommand()
        {
            return (ICommand)_instance.GetProperty("rotateCommand");
        }

        public void RemoveCurrentCommand()
        {
            _instance.RemoveProperty("rotateCommand");
        }

        public void SetCommand(ICommand command)
        {
            _instance.SetProperty("rotateCommand", command);
        }
    }
}