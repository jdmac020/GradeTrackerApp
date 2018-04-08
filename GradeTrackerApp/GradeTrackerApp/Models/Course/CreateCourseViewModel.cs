using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GradeTrackerApp.Models.Semester;

namespace GradeTrackerApp.Models.Course
{
    public class CreateCourseViewModel : IViewModel
    {
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