using GradeTrackerApp.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Course
{
    public class CourseWhatIfResultViewModel
    {
        public Guid CourseId { get; set; }

        [DisplayName("Course Name")]
        public string CourseName { get; set; }

        [DisplayName("What-If Final Grade")]
        public double WhatIfGrade { get; set; }
        public List<EvaluationWhatIfResultViewModel> Evaluations { get; set; }
        
    }
}