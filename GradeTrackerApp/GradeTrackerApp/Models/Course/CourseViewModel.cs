using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Models.Evaluation;
using GradeTrackerApp.Models.Semester;

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

        public double CurrentGrade { get; set; }

        public EvaluationListViewModel Evaluations { get; set; } = new EvaluationListViewModel();

        public SemesterViewModel Semester { get; set; }

        [DisplayName("Last Updated")]
        public DateTime LastModified { get; set; }

        public bool IsActive { get; set; }

        public CourseViewModel() { }

        public CourseViewModel(CourseDomainModel course)
        {
            Id = course.Id;
            Name = course.Name;
            Number = course.Number;
            Department = course.Department;
            Year = course.Year;
            IsActive = course.IsActive;
            CurrentGrade = course.CurrentPointsGrade;
            LastModified = course.LastUpdated ?? course.CreatedOn;

        }

        public void SetLastModified()
        {
            var possibleTimes = new List<DateTime>();

            if (LastModified != null)
                possibleTimes.Add((DateTime)LastModified);

            foreach (var eval in Evaluations)
            {
                var lastModified = new DateTime();

                if (eval.LastModified != null)
                    lastModified = (DateTime) eval.LastModified;

                possibleTimes.Add(lastModified);
            }

            possibleTimes.Sort();
            LastModified = possibleTimes.LastOrDefault();
        }
    }
}