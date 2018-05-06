using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class ObjectStateException : GradeTrackerException
    {
        private const string NAME = "Object State";

        public ObjectStateException()
        {

        }

        public ObjectStateException(string message) : base(NAME, message)
        {

        }

        public ObjectStateException(string message, Exception innerException) : base(NAME, message, innerException)
        {

        }
    }
}
