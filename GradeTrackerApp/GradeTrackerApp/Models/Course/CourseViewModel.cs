using System;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Semesters.Models;

namespace GradeTrackerApp.Models.Course
{
    public class CourseViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Number { get; set; }

        //public string SchoolName { get; set; }
        //public Guid? SchoolId { get; set; }
        //public string InstructorName { get; set; }
        //public Guid? InstructorId { get; set; }

        public int Year { get; set; }

        public string SemesterName
        {
            get { return Semester.Name; }
        }

        public Guid SemesterId
        {
            get { return Semester.Id; }
        }
        public SemesterDomainModel Semester { get; set; }

        //public DateTime? StartTime { get; set; }
        //public DateTime? EndTime { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        public DateTime? LastUpdated { get; set; }

        public CourseViewModel() { }

        public CourseViewModel(CourseDomainModel course)
        {
            Id = course.Id;
            Name = course.Name;
            Number = course.Number;
            Department = course.Department;
            //SchoolId = course.SchoolId;
            //InstructorId = course.InstructorId;
            Year = course.Year;
            //StartDate = course.StartDate;
            //StartTime = course.StartTime;
            //EndDate = course.EndDate;
            //EndTime = course.EndTime;
            LastUpdated = course.LastUpdated;
        }
    }
}