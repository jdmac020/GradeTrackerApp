using System;
using GradeTrackerApp.Domain.Courses.Models;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        CourseDomainModel CreateNewCourse(CreateCourseDomainModel createModel);
        CourseDomainModel GetCourse(Guid courseId);
    }
}