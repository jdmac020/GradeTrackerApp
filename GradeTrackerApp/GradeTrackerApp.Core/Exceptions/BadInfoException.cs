using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class BadInfoException : GradeTrackerException
    {
        private const string NAME = "Bad Info";

        public BadInfoException()
        {

        }

        public BadInfoException(string message) : base(NAME, message)
        {

        }

        public BadInfoException(string message, Exception innerException) : base(NAME, message, innerException)
        {

        }
    }
}
