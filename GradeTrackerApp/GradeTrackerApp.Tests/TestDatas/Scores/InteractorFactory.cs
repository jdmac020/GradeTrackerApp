using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Interactors.Score;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Scores
{
    public static class InteractorFactory
    {

        public static ScoreInteractor Create_ScoreInteractor()
        {
            var mockRepo = new MockRepository<ScoreEntity>();

            return new ScoreInteractor(mockRepo);
        }

        public static ScoreInteractor Create_ScoreInteractor(MockRepository<ScoreEntity> mockRepo)
        {
            return new ScoreInteractor(mockRepo);
        }

        public static IScoreInteractor Create_MockScoreInteractor()
        {
            return new MockScoreInteractor();
        }
    }
}
