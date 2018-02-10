using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Courses
{
    public class InteractorTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.Create_CourseInteractor();
            var testModel = new CourseEntity();

            Should.Throw<MissingInfoException>(() => testClass.CreateCourse(testModel));
        }

        [Fact]
        public void CreateCourse_ValidModel_ResultNotNull()
        {
            var testClass = InteractorFactory.Create_CourseInteractor();
            var testModel = CourseFactory.Create_CourseEntity_ValidMinimum();

            var result = testClass.CreateCourse(testModel);

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetCourse_EmptyModel_ThrowsObjectNotFoundException()
        {
            var testClass = InteractorFactory.Create_CourseInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetCourse(testGuid));
        }

        [Fact]
        public void GetCourse_ValidModel_ResultNotNull()
        {
            var testRepo = new MockRepository<CourseEntity>();
            var testCourse = CourseFactory.Create_CourseEntity_ValidMinimum();
            var testGuid = testRepo.Create(testCourse);

            var testClass = InteractorFactory.Create_CourseInteractor(testRepo);

            var result = testClass.GetCourse(testGuid);

            result.Name.ShouldNotBe(string.Empty);
        }
    }
}
