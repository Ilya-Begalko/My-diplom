namespace TanksGame.Commands.Abstractions
{
    /// <summary>
    ///     Абстрактная команда
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Метод, выполняющий полезную работу
        /// </summary>
        public void Execute();
    }
}