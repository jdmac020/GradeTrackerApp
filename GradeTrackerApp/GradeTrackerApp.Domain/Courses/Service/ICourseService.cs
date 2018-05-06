using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        IDomainModel CreateCourse(CreateCourseDomainModel createModel);
        IDomainModel GetCourse(Guid courseId);
        IDomainModel DeleteCourse(Guid courseId);
        IDomainModel UpdateCourse(CourseDomainModel updatedModel);
        List<IDomainModel> GetCourses(Guid userId);
    }
}