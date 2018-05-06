using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICourseInteractor
    {
        Guid CreateCourse(CourseEntity domainModel);
        void DeleteCourse(Guid courseId);
        void UpdateCourse(CourseEntity updatedCourse);
        CourseEntity GetCourse(Guid courseId);
        List<CourseEntity> GetAllCourses();
        List<CourseEntity> GetCoursesByStudentId(Guid userId);
    }
}