using System;
using TanksGame.Base;
using TanksGame.Commands.Abstractions;
using TanksGame.Contracts;
using Utils.Numeric;
using Utils.Numeric.Extensions;

namespace TanksGame.Commands.Rotate
{
    public class RotateVelocityCommand : ICommand
    {
        private readonly IMoveRotatable _moveRotateable;

        public RotateVelocityCommand(IMoveRotatable moveRotateable)
        {
            _moveRotateable = moveRotateable;
        }

        public void Execute()
        {
            SetVelocityWithCorrection();
            IoC.Resolve<object>("queue.add", this);
        }

        private void SetVelocityWithCorrection()
        {
            var currentVelocity = _moveRotateable.Velocity;
            _moveRotateable.Velocity = new Vector(
                (int)Math.Round(currentVelocity.GetNComponent(1) * Math.Cos(_moveRotateable.AngularVelocity.ToRadians()))
                - (int)Math.Round(currentVelocity.GetNComponent(2) *
                                  Math.Sin(_moveRotateable.AngularVelocity.ToRadians())),
                (int)Math.Round(currentVelocity.GetNComponent(2) * Math.Cos(_moveRotateable.AngularVelocity.ToRadians()))
                + (int)Math.Round(currentVelocity.GetNComponent(1) *
                                  Math.Sin(_moveRotateable.AngularVelocity.ToRadians()))
            );
        }
    }
}