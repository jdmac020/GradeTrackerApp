using System;

namespace GradeTrackerApp.Domain.Semesters.Models
{
    public class SemesterDomainModel : IDomainModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}