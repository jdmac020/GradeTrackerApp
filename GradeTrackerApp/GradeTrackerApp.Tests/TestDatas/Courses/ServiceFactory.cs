using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class ServiceFactory
    {
        public static CourseService CreateCourse_MockInteractor()
        {
            var interactor = new MockCourseInteractor();

            return new CourseService(interactor);
        }

        public static EvaluationService Create_EvaluationService()
        {
            var interactor = new MockEvaluationInteractor();

            return new EvaluationService(interactor);
        }
    }
}
