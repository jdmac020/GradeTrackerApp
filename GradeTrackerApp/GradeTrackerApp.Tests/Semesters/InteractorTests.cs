using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Semesters;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Semesters
{
    public class InteractorTests
    {
        [Fact]
        public void GetSemester_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = InteractorFactory.Create_SemesterInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetSemester(testGuid));
        }

        [Fact]
        public void GetSemester_ValidGuid_ReturnsSemester()
        {
            var mockRepo = new MockRepository<SemesterEntity>();
            var testGuid = mockRepo.Create(SemesterFactory.Create_SemesterEntity_ValidMinimum());
            var testClass = InteractorFactory.Create_SemesterInteractor(mockRepo);
            
            var result = testClass.GetSemester(testGuid);

            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldNotBe(string.Empty);

        }

        [Fact]
        public void GetAllSemsters_ReturnsThreeSemesters()
        {
            var mockRepo = new MockRepository<SemesterEntity>();
            var list = SemesterFactory.Create_SemesterEntity_ListOfAll();

            foreach (var semester in list)
            {
                mockRepo.Create(semester);
            }

            var testClass = InteractorFactory.Create_SemesterInteractor(mockRepo);

            var result = testClass.GetAllSemesters();

            result.Count.ShouldBe(3);
        }
        
    }
}
