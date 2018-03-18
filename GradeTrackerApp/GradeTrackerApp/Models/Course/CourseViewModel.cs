using System;
using GradeTrackerApp.Domain.Courses.Models;

namespace GradeTrackerApp.Models.Course
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public string SchoolName { get; set; }
        public Guid? SchoolId { get; set; }
        public string InstructorName { get; set; }
        public Guid? InstructorId { get; set; }
        public int Year { get; set; }
        public string SemesterName { get; set; }
        public Guid SemesterId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CourseViewModel() { }

        public CourseViewModel(CourseDomainModel course)
        {
            Name = course.Name;
            Number = course.Number;
            Department = course.Department;
            SchoolId = course.SchoolId;
            InstructorId = course.InstructorId;
            Year = course.Year;
            SemesterId = course.SemesterId;
            StartDate = course.StartDate;
            StartTime = course.StartTime;
            EndDate = course.EndDate;
            EndTime = course.EndTime;
        }
    }
}