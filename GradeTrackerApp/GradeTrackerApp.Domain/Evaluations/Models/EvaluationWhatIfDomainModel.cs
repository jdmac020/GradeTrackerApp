using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Domain.Evaluations.Models
{
    public class EvaluationWhatIfDomainModel
    {
        public Guid EvaluationId { get; set; }
        public double WhatIfGrade { get; set; }
    }
}
