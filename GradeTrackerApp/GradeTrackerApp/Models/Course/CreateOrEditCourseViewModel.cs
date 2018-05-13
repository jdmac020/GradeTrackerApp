using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Models.Semester;

namespace GradeTrackerApp.Models.Course
{
    public class CreateOrEditCourseViewModel : IViewModel
    {
        public CreateOrEditCourseViewModel() { }

        public CreateOrEditCourseViewModel(CourseDomainModel domainModel)
        {
            Id = domainModel.Id;
            StudentId = domainModel.StudentId;
            Name = domainModel.Name;
            Department = domainModel.Department;
            Number = domainModel.Number;
            Year = domainModel.Year.ToString();
            SemesterId = domainModel.SemesterId;

        }

        public Guid? Id { get; set; }
        public Guid StudentId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "*Every Course Need a Name*")]
        public string Name { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "*We need a Department*")]
        public string Department { get; set; }

        [DisplayName("Number")]
        [Required(ErrorMessage = "*Every Course Needs a Number*")]
        public string Number { get; set; }

        //public Guid? SchoolId { get; set; }
        //public Guid? InstructorId { get; set; }
        [DisplayName("Year")]
        public string Year { get; set; }

        [DisplayName("Semester")]
        public Guid SemesterId { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public List<SelectListItem> YearOptions { get; set; }

        //[DataType(DataType.Time)]
        //public string StartTime
        //{
        //    get { return _startTime.ToShortTimeString();}
        //    set { _startTime = DateTime.Parse(value); }
        //}

        //[DataType(DataType.Time)]
        //public string EndTime
        //{
        //    get { return _endTime.ToShortTimeString();}
        //    set { _endTime = DateTime.Parse(value);}
        //}

        //[DataType(DataType.Date)]
        //public string StartDate
        //{
        //    get { return _startDate.ToShortDateString(); }
        //    set { _startDate = DateTime.Parse(value); }
        //}

        //[DataType(DataType.Date)]
        //public string EndDate
        //{
        //    get { return _endDate.ToShortDateString();}
        //    set { _endDate = DateTime.Parse(value); }
        //}

        public List<SelectListItem> Semesters { get; set; }

        //private DateTime _startDate = DateTime.Today;
        //private DateTime _endDate = DateTime.Today.AddDays(120);
        //private DateTime _startTime = DateTime.Today.AddHours(8);
        //private DateTime _endTime = DateTime.Today.AddHours(10);

        
    }
}