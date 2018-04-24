using System;
using GradeTrackerApp.Core.Exceptions;

namespace GradeTrackerApp.Domain.Shared
{
    public class ErrorDomainModel : IDomainModel
    {
        public string Name { get; set; }
        public bool Retry { get; set; }
        public GradeTrackerException Exception { get; set; }

        public ErrorDomainModel(GradeTrackerException exception, bool canRetry)
        {
            Name = exception.Name;
            Exception = exception;
            Retry = canRetry;
        }
    }
}