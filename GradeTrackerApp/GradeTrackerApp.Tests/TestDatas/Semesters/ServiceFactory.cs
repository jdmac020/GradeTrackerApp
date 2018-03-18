using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Semesters
{
    public static class ServiceFactory
    {
        
        public static SemesterService Create_SemesterService()
        {
            var interactor = new MockSemesterInteractor();

            return new SemesterService(interactor);
        }
    }
}
