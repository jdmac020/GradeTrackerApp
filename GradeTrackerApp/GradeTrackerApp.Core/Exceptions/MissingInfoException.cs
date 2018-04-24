using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class MissingInfoException : GradeTrackerException
    {
        private const string NAME = "Missing Info";

        public MissingInfoException()
        {

        }

        public MissingInfoException(string message) : base(NAME, message)
        {

        }

        public MissingInfoException(string message, Exception innerException) : base(NAME, message, innerException)
        {

        }
    }
}
