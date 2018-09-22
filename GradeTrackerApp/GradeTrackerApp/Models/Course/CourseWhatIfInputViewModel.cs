using GradeTrackerApp.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Course
{
    public class CourseWhatIfInputViewModel
    {
        public string CourseId { get; set; }
        public IEnumerable<EvaluationWhatIfViewModel> Evaluations { get; set; }
    }
}