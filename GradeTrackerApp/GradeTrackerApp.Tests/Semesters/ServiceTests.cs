using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Semesters;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Semesters
{
    public class ServiceTests
    {
        

        [Fact]
        public void GetSemester_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = ServiceFactory.Create_SemesterService();
            var testGuid = Guid.Empty;

            var result = testClass.GetSemester(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetSemester_ValidGuid_GetsValidModel()
        {
            var testRepo = new MockRepository<SemesterEntity>();
            var testSemester = SemesterFactory.Create_SemesterEntity_ValidMinimum();
            var testGuid = testRepo.Create(testSemester);

            var testClass = ServiceFactory.Create_SemesterService();

            var result = testClass.GetSemester(testGuid);
            
            result.Name.ShouldNotBe(string.Empty);

        }

        [Fact]
        public void GetAllSemesters_ValidCall_ReturnsThreeSemesters()
        {
            var testRepo = new MockRepository<SemesterEntity>();
            var testSemesters = SemesterFactory.Create_SemesterEntity_ListOfAll();

            foreach (var semesterEntity in testSemesters)
            {
                testRepo.Create(semesterEntity);
            }

            var testClass = ServiceFactory.Create_SemesterService();

            var result = testClass.GetAllSemesters();

            result.Count.ShouldBe(3);
        }
    }
}
