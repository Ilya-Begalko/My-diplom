using System;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using Utils.Numeric;
using Utils.Numeric.Extensions;

namespace TanksGame.Commands.Rotate
{
    ///     Комманда поворота вокруг собственной оси
    public class RotateSelfCommand : ICommand
    {
        private readonly IRotatable _rotatable;

        public RotateSelfCommand(IRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        public void Execute()
        {
            SetDirectionWithCorrection();
            IoC.Resolve<object>("queue.add", this);
        }

        private void SetDirectionWithCorrection()
        {
            var currentDirection = _rotatable.Direction;

            _rotatable.Direction = new Vector(
                (int)Math.Round(currentDirection.GetNComponent(1) * Math.Cos(_rotatable.AngularVelocity.ToRadians()))
                - (int)Math.Round(currentDirection.GetNComponent(2) * Math.Sin(_rotatable.AngularVelocity.ToRadians())),
                (int)Math.Round(currentDirection.GetNComponent(2) * Math.Cos(_rotatable.AngularVelocity.ToRadians()))
                + (int)Math.Round(currentDirection.GetNComponent(1) * Math.Sin(_rotatable.AngularVelocity.ToRadians()))
            );
        }
    }
}