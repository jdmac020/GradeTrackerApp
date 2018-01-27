using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICourseInteractor
    {
        Guid CreateCourse(CourseEntity domainModel);

        CourseEntity GetCourseById(Guid courseId);
    }
}