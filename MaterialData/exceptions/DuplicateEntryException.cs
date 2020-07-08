using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.exceptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException()
        {
        }

        public DuplicateEntryException(string message) : base(message)
        {
        }

        public DuplicateEntryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}