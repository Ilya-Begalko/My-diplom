using System;

namespace TanksGame.Base.Exceptions
{
    public class ExecutException : Exception
    {
        public ExecutException() { }
        public ExecutException(string message) : base(message) { }
    }
}