using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.TestDatas.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Evaluations
{
    public class EvaluationInteractorTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.CreateCourse_MockRepo();
            var testModel = new CourseEntity();

            Should.Throw<MissingInfoException>(() => testClass.CreateCourse(testModel));
        }

        [Fact]
        public void CreateCourse_ValidModel_ResultNotNull()
        {
            var testClass = InteractorFactory.CreateCourse_MockRepo();
            var testModel = CourseFactory.Create_CourseEntity_ValidMinimum();

            var result = testClass.CreateCourse(testModel);

            result.ShouldNotBeNull();
        }
    }
}
