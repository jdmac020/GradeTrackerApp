using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
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
        //public Guid? SchoolId { get; set; }
        //public Guid? InstructorId { get; set; }
        public int Year { get; set; }
        public Guid SemesterId { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public double TotalPointsPossible { get; set; }
        public double CurrentPointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public int EvaluationCount { get; set; }
        public List<Guid> Evaluations { get; set; }
        public List<Guid> GradeRanges { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public string CurrentLetterGrade { get; set; }
        public string FinalLetterGrade { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime CreatedOn { get; set; }

        public CourseDomainModel() { }

        public CourseDomainModel(CourseEntity courseEntity)
        {
            Id = courseEntity.Id;
            Name = courseEntity.Name;
            Department = courseEntity.Department;
            Number = courseEntity.Number;
            //SchoolId = courseEntity.SchoolId;
            //InstructorId = courseEntity.InstructorId;
            Year = courseEntity.Year;
            SemesterId = courseEntity.SemesterId;
            //StartTime = courseEntity.StartTime;
            //EndTime = courseEntity.EndTime;
            //StartDate = courseEntity.StartDate;
            //EndDate = courseEntity.EndDate;
            EvaluationCount = courseEntity.EvaluationCount;
            CurrentPointsGrade = courseEntity.CurrentPointsGrade;
            FinalPointsGrade = courseEntity.FinalPointsGrade;
            CurrentLetterGrade = courseEntity.CurrentLetterGrade;
            FinalLetterGrade = courseEntity.FinalLetterGrade;
            LastUpdated = courseEntity.LastModified;
            CreatedOn = courseEntity.CreatedOn;
        }

        
    }
}