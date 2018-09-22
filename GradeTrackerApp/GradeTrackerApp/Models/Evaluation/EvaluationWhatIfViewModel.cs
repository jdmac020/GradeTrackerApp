using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Evaluation
{
    public class EvaluationWhatIfViewModel
    {
        public string EvaluationId { get; set; }
        public double PointsEarned { get; set; }
        public double PointsPossible { get; set; }
    }
}