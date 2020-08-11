using System;

namespace Akan.Exceptions
{
    public class IllegalAccessException : System.Exception
    {
        public IllegalAccessException(string message) : base(message)
        {}
    }
}