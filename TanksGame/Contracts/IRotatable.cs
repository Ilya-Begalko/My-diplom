using Utils.Numeric;

namespace TanksGame.Contracts
{
    /// <summary>
    ///     Объект, умеющий поворачивать
    /// </summary>
    public interface IRotatable : IManageable
    {
        Vector Direction { get; set; }
        int AngularVelocity { get; set; }
    }
}