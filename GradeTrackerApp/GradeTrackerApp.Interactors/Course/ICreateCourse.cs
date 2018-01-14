using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.EntityFramework.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    interface ICreateCourse
    {
        CourseEntity Execute(CreateCourseDomainModel domainModel);
    }
}
