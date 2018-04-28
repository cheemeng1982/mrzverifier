using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassportValidator
{
    public class PassportDataException : Exception
    {
        public PassportDataException()
        {
        }

        public PassportDataException(string message)
            : base(message)
        {
        }

        public PassportDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}