using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Evaluations
{
    public static class ServiceFactory
    {  

        public static EvaluationService Create_EvaluationService()
        {
            var interactor = new MockEvaluationInteractor();

            return new EvaluationService(interactor);
        }
    }
}
