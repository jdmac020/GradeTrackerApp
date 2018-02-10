using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Interactors.Score;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class InteractorFactory
    {
        public static CourseInteractor CreateCourse_MockRepo()
        {
            var mockRepo = new MockRepository<CourseEntity>();

            return new CourseInteractor(mockRepo);
        }

        public static ICourseInteractor CreateCourse_MockInteractor()
        {
            return new MockCourseInteractor();
        }
    }
}
