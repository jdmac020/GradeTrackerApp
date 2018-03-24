using System;
using System.ComponentModel;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Semesters.Models;

namespace GradeTrackerApp.Models.Course
{
    public class CourseViewModel
    {
        public Guid? Id { get; set; }

        public Guid StudentId { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }

        [DisplayName("Number")]
        public string Number { get; set; }

        [DisplayName("Academic Year")]
        public int Year { get; set; }

        [DisplayName("Semester")]
        public string SemesterName
        {
            get { return Semester.Name; }
        }

        public Guid SemesterId
        {
            get { return Semester.Id; }
        }

        public SemesterDomainModel Semester { get; set; }

        [DisplayName("Last Updated")]
        public string LastUpdated
        {
            get { return $"{_lastModified.ToShortTimeString()}, {_lastModified.ToShortDateString()}";}
            set { _lastModified = DateTime.Parse(value); }
        }

        protected DateTime _lastModified;

        public CourseViewModel() { }

        public CourseViewModel(CourseDomainModel course)
        {
            Id = course.Id;
            Name = course.Name;
            Number = course.Number;
            Department = course.Department;
            Year = course.Year;

            if (course.LastUpdated != null)
            {
                _lastModified = (DateTime)course.LastUpdated;
            }
            
        }
    }
}