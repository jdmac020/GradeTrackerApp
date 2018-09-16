using GradeTrackerApp.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Domain.Courses.Models
{
    public class CourseWeightDomainModel
    {
        public bool IsWeighted { get; set; }
        public bool IsStraightPoints { get; set; }
    }
}
