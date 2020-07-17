using System;

namespace MaterialData.exceptions
{
    public class DuplcateEntryException : Exception
    {
        public DuplcateEntryException()
        {
        }

        public DuplcateEntryException(string message) : base(message)
        {
        }

        public DuplcateEntryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}