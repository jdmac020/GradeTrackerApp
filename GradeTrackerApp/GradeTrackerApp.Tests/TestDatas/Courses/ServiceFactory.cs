using GradeTrackerApp.Domain.Courses.Service;
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
    }
}
