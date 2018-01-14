using System;
using GradeTrackerApp.EntityFramework.Entities;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Tests.Mocks
{
    public static class InteractorFactory
    {
        public static CreateCourse CreateCourse_MockRepo()
        {
            var mockRepo = new MockRepository<CourseEntity, Guid>();

            return new CreateCourse(mockRepo);
        }
    }
}
