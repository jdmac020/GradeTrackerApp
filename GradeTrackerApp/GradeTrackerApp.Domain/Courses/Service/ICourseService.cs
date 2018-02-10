using System;
using GradeTrackerApp.Domain.Courses.Models;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        CourseDomainModel CreateCourse(CreateCourseDomainModel createModel);
        CourseDomainModel GetCourse(Guid courseId);
    }
}