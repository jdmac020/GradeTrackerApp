using ConjureGrade.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Domain.Courses.Models
{
    public class CourseConjureGradeResultModel : CourseResult
    {
        public Guid? CourseId { get; set; }
    }
}
