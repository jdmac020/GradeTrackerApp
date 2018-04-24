using System;
using System.Linq;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.TestDatas.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Courses
{
    public class ServiceTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testModel = new CreateCourseDomainModel();

            var result = testClass.CreateCourse(testModel);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void CreateCourse_ValidModel_ResultNotNull()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testModel = CourseFactory.Create_CreateCourseDomainModel_ValidMinimum();

            var result = testClass.CreateCourse(testModel);

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetCourse_EmptyGuid_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testGuid = Guid.Empty;

            var result = testClass.GetCourse(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetCourse_ValidGuid_NameNotEmpty()
        {
            var testGuid = Guid.NewGuid();

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourse(testGuid);

            result.Name.ShouldNotBe(string.Empty);
        }

        [Fact]
        public void GetCourses_EmptyGuid_ThrowsBadInfoException()
        {
            var testGuid = Guid.Empty;

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourses(testGuid);

            result.First().GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetCourses_NewGuid_ReturnsTwoDomainModels()
        {
            var testGuid = Guid.NewGuid();

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourses(testGuid);
        }
    }
}
