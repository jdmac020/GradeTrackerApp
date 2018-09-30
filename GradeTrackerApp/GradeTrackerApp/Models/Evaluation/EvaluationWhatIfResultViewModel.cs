using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Evaluation
{
    public class EvaluationWhatIfResultViewModel
    {
        public Guid EvaluationId { get; set; }

        [DisplayName("Evaluation Type")]
        public string EvaluationName { get; set; }

        [DisplayName("What-If Grade")]
        public double WhatIfGrade { get; set; }
    }
}