using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Models.Courses;
using GradeTrackerApp.Interactors.Course;
using Xunit;
using Shouldly;

namespace GradeTrackerApp.Tests.Interactors.Course
{
    public class CreateCourseTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.CreateCourse();
            var testModel = new CreateCourseModel();

            Should.Throw<MissingInfoException>(() => testClass.Execute(testModel));
        }
    }
}
