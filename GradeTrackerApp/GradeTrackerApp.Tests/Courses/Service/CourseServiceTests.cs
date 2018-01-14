using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Core.Models;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestValues.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Courses.Service
{
    public class CourseServiceTests
    {
        [Fact]
        public void CreateNewCourse_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = ServiceFactory.CreateCourse_MockInteractor();
            var testModel = new CreateCourseDomainModel();

            Should.Throw<MissingInfoException>(() => testClass.CreateNewCourse(testModel));
        }

        [Fact]
        public void CreateNewCourse_ValidModel_ResultNotNull()
        {
            var testClass = ServiceFactory.CreateCourse_MockInteractor();
            var testModel = CourseFactory.Create_CreateCourseDomainModel_ValidMinimum();

            var result = testClass.CreateNewCourse(testModel);

            result.ShouldNotBeNull();
        }
    }
}
