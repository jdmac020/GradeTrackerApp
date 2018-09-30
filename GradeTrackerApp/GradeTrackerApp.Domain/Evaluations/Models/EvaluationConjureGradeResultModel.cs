using ConjureGrade.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Domain.Evaluations.Models
{
    public class EvaluationConjureGradeResultModel : EvaluationResult
    {
        public Guid? EvaluationId { get; set; }
        public string EvaluationName { get; set; }
    }
}
