using System;

namespace MaterialData.exceptions
{
    public class NotAddedButUpdatedException : Exception
    {
        public NotAddedButUpdatedException()
        {
        }

        public NotAddedButUpdatedException(string message) : base(message)
        {
        }

        public NotAddedButUpdatedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}