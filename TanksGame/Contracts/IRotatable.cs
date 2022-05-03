using Utils.Numeric;

namespace TanksGame.Contracts
{
    public interface IRotatable : IManageable
    {
        Vector Direction { get; set; }
        int AngularVelocity { get; set; }
    }
}