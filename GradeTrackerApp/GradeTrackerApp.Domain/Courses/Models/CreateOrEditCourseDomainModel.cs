using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Domain.Courses.Models
{
    public class CreateOrEditCourseDomainModel
    {
        public Guid? Id { get; set; }
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public Guid SemesterId { get; set; }
        public int Year { get; set; }

        public CreateOrEditCourseDomainModel() { }

        public CreateOrEditCourseDomainModel(CourseEntity courseEntity)
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
            ////EndDate = courseEntity.EndDate;
            //EvaluationCount = courseEntity.EvaluationCount;
            //CurrentPointsGrade = courseEntity.CurrentPointsGrade;
            //FinalPointsGrade = courseEntity.FinalPointsGrade;
            //CurrentLetterGrade = courseEntity.CurrentLetterGrade;
            //FinalLetterGrade = courseEntity.FinalLetterGrade;
            //LastUpdated = courseEntity.LastModified;
            //CreatedOn = courseEntity.CreatedOn;
            //IsActive = courseEntity.IsActive;
        }

        //public Guid? SchoolId { get; set; }
        //public Guid? InstructorId { get; set; }
        //public DateTime? StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
    }
}