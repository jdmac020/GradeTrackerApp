using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class InteractorFactory
    {
        public static CourseInteractor CreateCourse_MockRepo()
        {
            var mockRepo = new MockRepository<CourseEntity, Guid>();

            return new CourseInteractor(mockRepo);
        }

        public static ICourseInteractor CreateCourse_MockInteractor()
        {
            return new MockCourseInteractor();
        }
    }
}
