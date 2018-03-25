using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Semesters.Models;

namespace GradeTrackerApp.Models.Semester
{
    public class SemesterViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SemesterViewModel(SemesterDomainModel domainModel)
        {
            Id = domainModel.Id;
            Name = domainModel.Name;
        }

        
    }
}