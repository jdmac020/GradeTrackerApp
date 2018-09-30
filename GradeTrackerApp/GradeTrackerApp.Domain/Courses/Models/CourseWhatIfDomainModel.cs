using GradeTrackerApp.Domain.Evaluations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Domain.Courses.Models
{
    public class CourseWhatIfDomainModel
    {
        public Guid CourseId { get; set; }
        public double WhatIfGrade { get; set; }
        public List<EvaluationWhatIfDomainModel> WhatIfEvaluations { get; set; }
    }
}
