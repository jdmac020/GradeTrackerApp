using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Courses.Models;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        CourseDomainModel CreateNewCourse(CreateCourseDomainModel viewModel);
    }
}
