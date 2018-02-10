using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class BadInfoException : Exception
    {
        public BadInfoException()
        {

        }

        public BadInfoException(string message) : base(message)
        {

        }

        public BadInfoException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
