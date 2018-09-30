using GradeTrackerApp.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Course
{
    public class WhatIfEditViewModel
    {
        public Guid CourseId { get; set; }
        public List<EvaluationWhatIfViewModel> Evaluations { get; set; }
    }
}