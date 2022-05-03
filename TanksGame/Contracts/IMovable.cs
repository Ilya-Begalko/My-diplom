using Utils.Numeric;

namespace TanksGame.Contracts
{
    public interface IMovable : IManageable
    {
        Vector Position { get; set; }
        Vector Velocity { get; set; }
    }
}