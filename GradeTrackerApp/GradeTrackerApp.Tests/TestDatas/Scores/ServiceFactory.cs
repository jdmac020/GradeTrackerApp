using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Scores.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Scores
{
    public static class ServiceFactory
    {
        
        public static ScoreService Create_ScoreService()
        {
            var interactor = new MockScoreInteractor();

            return new ScoreService(interactor);
        }
    }
}
