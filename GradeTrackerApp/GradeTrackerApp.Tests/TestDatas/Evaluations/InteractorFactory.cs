using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Interactors.Score;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Evaluations
{
    /// <summary>
    /// Handy Static Helpers for Creating EvaluationInteractors
    /// </summary>
    public static class InteractorFactory
    {
        
        public static EvaluationInteractor Create_EvaluationInteractor()
        {
            var mockRepo = new MockRepository<EvaluationEntity>();

            return new EvaluationInteractor(mockRepo);
        }

        public static EvaluationInteractor Create_EvaluationInteractor(MockRepository<EvaluationEntity> mockRepo)
        {
            return new EvaluationInteractor(mockRepo);
        }

        public static IEvaluationInteractor Create_MockEvaluationInteractor()
        {
            return new MockEvaluationInteractor();
        }

        
    }
}
