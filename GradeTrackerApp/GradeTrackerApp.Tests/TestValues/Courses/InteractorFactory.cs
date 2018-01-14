using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestValues.Courses
{
    public static class InteractorFactory
    {
        public static CreateCourse CreateCourse_MockRepo()
        {
            var mockRepo = new MockRepository<CourseEntity, Guid>();

            return new CreateCourse(mockRepo);
        }

        public static ICreateCourse CreateCourse_MockInteractor()
        {
            return new MockCreateCourse();
        }
    }
}
