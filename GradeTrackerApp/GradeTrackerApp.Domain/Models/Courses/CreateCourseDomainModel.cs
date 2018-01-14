using System;
using System.Collections.Generic;

namespace GradeTrackerApp.Domain.Models.Courses
{
    public class CreateCourseDomainModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? InstructorId { get; set; }
        public int Year { get; set; }
        public Guid SemesterId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EvaluationCount { get; set; }
    }
}
