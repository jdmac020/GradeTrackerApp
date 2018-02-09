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
            var mockRepo = new MockRepository<CourseEntity, Guid>();

            return new CourseInteractor(mockRepo);
        }

        public static ICourseInteractor CreateCourse_MockInteractor()
        {
            return new MockCourseInteractor();
        }

        public static EvaluationInteractor Create_EvaluationInteractor()
        {
            var mockRepo = new MockRepository<EvaluationEntity, Guid>();

            return new EvaluationInteractor(mockRepo);
        }

        public static EvaluationInteractor Create_EvaluationInteractor(MockRepository<EvaluationEntity, Guid> mockRepo)
        {
            return new EvaluationInteractor(mockRepo);
        }

        public static IEvaluationInteractor Create_MockEvaluationInteractor()
        {
            return new MockEvaluationInteractor();
        }

        public static ScoreInteractor Create_ScoreInteractor()
        {
            var mockRepo = new MockRepository<EvaluationEntity, Guid>();

            return new ScoreInteractor(mockRepo);
        }

        public static ScoreInteractor Create_ScoreInteractor(MockRepository<EvaluationEntity, Guid> mockRepo)
        {
            return new ScoreInteractor(mockRepo);
        }

        public static IScoreInteractor Create_MockScoreInteractor()
        {
            return new MockScoreInteractor();
        }
    }
}
