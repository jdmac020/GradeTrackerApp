using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Courses.Models;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        CourseDomainModel CreateCourse(CreateCourseDomainModel createModel);
        CourseDomainModel GetCourse(Guid courseId);
        List<CourseDomainModel> GetCourses(Guid userId);
    }
}