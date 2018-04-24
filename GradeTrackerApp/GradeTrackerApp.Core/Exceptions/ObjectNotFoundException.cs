using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class ObjectNotFoundException : GradeTrackerException
    {
        private const string NAME = "Object Not Found";

        public ObjectNotFoundException()
        {

        }

        public ObjectNotFoundException(string message) : base(NAME, message)
        {

        }

        public ObjectNotFoundException(string message, Exception innerException) : base(NAME, message, innerException)
        {

        }
    }
}
