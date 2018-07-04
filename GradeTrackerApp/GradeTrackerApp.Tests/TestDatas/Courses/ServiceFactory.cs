using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class ServiceFactory
    {
        public static CourseService Create_MockInteractor()
        {
            var service = new CourseService
            {
                CourseInteractor = new MockCourseInteractor(),
                EvaluationService = new MockEvaluationService_Empties()
            };

            return service;
        }
    }
}
