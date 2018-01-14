using System;
using System.Collections.Generic;

namespace GradeTrackerApp.Core.Entities
{
    public class CourseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
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
        public double TotalPointsPossible { get; set; }
        public double CurrentPointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public int EvaluationCount { get; set; }
        public virtual List<Guid> EvaluationIds { get; set; }
        public virtual List<Guid> GradeRangeIds { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
