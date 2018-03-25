using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class MissingInfoException : Exception
    {
        public MissingInfoException()
        {

        }

        public MissingInfoException(string message) : base(message)
        {

        }

        public MissingInfoException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
