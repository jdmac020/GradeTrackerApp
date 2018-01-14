using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Tests.TestValues.Courses;
using Xunit;
using Shouldly;

namespace GradeTrackerApp.Tests.Interactors.Course
{
    public class CreateCourseTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.CreateCourse_MockRepo();
            var testModel = new CreateCourseDomainModel();

            Should.Throw<MissingInfoException>(() => testClass.Execute(testModel));
        }

        [Fact]
        public void CreateCourse_ValidModel_ResultNotNull()
        {
            var testClass = InteractorFactory.CreateCourse_MockRepo();
            var testModel = CourseFactory.Create_CreateCourseDomainModel_ValidMinimum();

            var result = testClass.Execute(testModel);

            result.ShouldNotBeNull();
        }
    }
}
