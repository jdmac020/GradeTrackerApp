using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Models;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICreateCourse
    {
        CourseEntity Execute(CreateCourseDomainModel domainModel);
    }
}
