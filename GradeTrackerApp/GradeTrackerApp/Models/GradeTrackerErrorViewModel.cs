using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Models
{
    public class GradeTrackerErrorViewModel
    {
        public string Name { get; set; }
        public bool Retry { get; set; }
        public IViewModel ViewModel {get; set; }
        public Exception Exception { get; set; }

        public GradeTrackerErrorViewModel(ErrorDomainModel domainModel)
        {
            Name = domainModel.Name;
            Retry = domainModel.Retry;
            Exception = domainModel.Exception;
        }
    }
}