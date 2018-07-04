using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Evaluations
{
    public static class ServiceFactory
    {  

        public static EvaluationService Create_EvaluationService()
        {
            var evalInteractor = new MockEvaluationInteractor();

            var scoreInteractor = new MockScoreInteractor();

            var service = new EvaluationService();

            service.EvaluationInteractor = evalInteractor;
            service.ScoreInteractor = scoreInteractor;

            return service;
        }
    }
}
