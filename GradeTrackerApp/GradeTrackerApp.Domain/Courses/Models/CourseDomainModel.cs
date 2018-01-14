﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.GradeRanges.Models;
using GradeTrackerApp.Domain.Instructors.Models;
using GradeTrackerApp.Domain.Semesters.Models;

namespace GradeTrackerApp.Domain.Courses.Models
{
    public class CourseDomainModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? InstructorId { get; set; }
        public virtual InstructorDomainModel Instructor {get; set;}
        public int Year { get; set; }
        public Guid SemesterId { get; set; }
        public virtual SemesterDomainModel Semester { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPointsPossible { get; set; }
        public double CurrentPointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public int EvaluationCount { get; set; }
        public virtual List<Guid> EvaluationIds { get; set; }
        public virtual List<EvaluationDomainModel> Evaluations { get; set; }
        public virtual List<Guid> GradeRangeIds { get; set; }
        public virtual List<GradeRangeDomainModel> GradeRanges { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
