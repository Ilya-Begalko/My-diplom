using System;

namespace TanksGame.Base.Exceptions
{
     public class CollisionException : Exception
    {
        public CollisionException() { }
        public CollisionException(string message) : base(message) { }
    }
}