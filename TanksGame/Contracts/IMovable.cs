using Utils.Numeric;

namespace TanksGame.Contracts
{
    /// <summary>
    ///     Объект, умеющий двигаться
    /// </summary>
    public interface IMovable : IManageable
    {
        Vector Position { get; set; }
        Vector Velocity { get; set; }
    }
}