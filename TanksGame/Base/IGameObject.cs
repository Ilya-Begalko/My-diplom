namespace TanksGame.Base
{
    public interface IGameObject
    {
        public object GetProperty(string name);
        public void SetProperty(string name, object instance);
        public void RemoveProperty(string name);
    }
}