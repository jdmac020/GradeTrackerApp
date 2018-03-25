using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Scores;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Scores
{
    public class InteractorTests
    {
        [Fact]
        public void CreateScore_EmptyEntity_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.Create_ScoreInteractor();
            var emptyScore = ScoreFactory.Create_ScoreEntity_Empty();

            Should.Throw<MissingInfoException>(() => testClass.CreateScore(emptyScore));
        }

        [Fact]
        public void CreateScore_MinimumEntity_ReturnsNewGuid()
        {
            var testClass = InteractorFactory.Create_ScoreInteractor();
            var testScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();

            var result = testClass.CreateScore(testScore);

            result.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void CreateScore_DuplicateEntry_ThrowsObjectAlreadyExists()
        {
            var testRepo = new MockRepository<ScoreEntity>();
            var testScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();
            var testGuid = testRepo.Create(testScore);

            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);
            var duplicateScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();

            Should.Throw<ObjectAlreadyExistsException>(() => testClass.CreateScore(duplicateScore));
        }

        [Fact]
        public void GetScore_EmptyGuid_ThrowsObjectNotFoundException()
        {
            var testClass = InteractorFactory.Create_ScoreInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetScore(testGuid));
        }

        [Fact]
        public void GetScore_ExistingScore_ReturnsScore()
        {
            var testRepo = new MockRepository<ScoreEntity>();
            var testScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();
            var testGuid = testRepo.Create(testScore);

            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);

            var result = testClass.GetScore(testGuid);

            result.Name.ShouldNotBe(string.Empty);
            result.Id.ShouldBe(testGuid);
        }
    }
}
