using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GradeTrackerApp.Models.Semester;

namespace GradeTrackerApp.Models.Course
{
    public class CreateCourseViewModel
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? InstructorId { get; set; }
        public int Year { get; set; }
        public Guid SemesterId { get; set; }

        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public List<SelectListItem> Semesters { get; set; }
    }
}