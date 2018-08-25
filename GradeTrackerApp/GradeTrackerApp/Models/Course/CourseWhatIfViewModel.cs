using GradeTrackerApp.Models.Evaluation;
using GradeTrackerApp.Models.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Course
{
    public class CourseWhatIfViewModel : IViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<EvaluationViewModel> EvaluationList { get; set; }

        public List<ScoreViewModel> ScoreList { get; set; }
    }
}