﻿using System;
using System.Collections.Generic;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class CourseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public Guid SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }
        public Guid? InstructorId { get; set; }
        public virtual InstructorEntity Instructor { get; set; }
        public int Year { get; set; }
        public Guid SemesterId { get; set; }
        public virtual SemesterEntity Semester { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPointsPossible { get; set; }
        public double CurrentPointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public int EvaluationCount { get; set; }
        public virtual List<EvaluationEntity> Evaluations { get; set; }
        public virtual List<GradeThresholdEntity> GradeRanges { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
