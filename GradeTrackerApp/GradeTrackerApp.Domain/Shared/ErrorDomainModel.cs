using System;

namespace GradeTrackerApp.Domain.Shared
{
    public class ErrorDomainModel : IDomainModel
    {
        public string Name { get; set; }
        public bool Retry { get; set; }
        public Exception Exception { get; set; }

        public ErrorDomainModel(string errorName, Exception exception, bool canRetry)
        {
            Name = errorName;
            Exception = exception;
            Retry = canRetry;
        }
    }
}