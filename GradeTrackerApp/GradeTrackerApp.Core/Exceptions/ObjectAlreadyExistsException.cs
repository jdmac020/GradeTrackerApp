﻿using System;

namespace GradeTrackerApp.Core.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException()
        {

        }

        public ObjectAlreadyExistsException(string message) : base(message)
        {

        }

        public ObjectAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
