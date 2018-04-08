using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class ObjectAlreadyExistsException : GradeTrackerException
    {
        private const string NAME = "Object Already Exists";

        public ObjectAlreadyExistsException()
        {

        }

        public ObjectAlreadyExistsException(string message) : base(NAME, message)
        {

        }

        public ObjectAlreadyExistsException(string message, Exception innerException) : base(NAME, message, innerException)
        {

        }
    }
}
