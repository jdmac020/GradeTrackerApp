using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Models;
using GradeTrackerApp.EntityFramework.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICreateCourse
    {
        CourseEntity Execute(CreateCourseDomainModel domainModel);
    }
}
