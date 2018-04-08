using System;
using System.Web.Mvc;
using GradeTrackerApp.Controllers;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Courses
{
    public class ControllerTests
    {
        //[Fact]
        //public void CreateCourse_EmptyModel_ThrowsMissingInfoException()
        //{
        //    var testClass = new CourseController(new MockCourseService_Positives(), new MockEvaluationService_Empties(), new MockSemesterService_Fails());
        //    var testGuid = Guid.Empty;

        //    ViewResult result = (ViewResult)testClass.ViewCourse(testGuid);

        //    result.View.ToString().ShouldContain("GradeTrackerError");

            
        //}
    }
}
