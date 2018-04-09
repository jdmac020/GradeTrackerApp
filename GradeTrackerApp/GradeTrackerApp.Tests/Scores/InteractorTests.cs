using System;
using System.Linq;
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

        [Fact]
        public void GetScoresByEvalId_ExistingScores_ReturnsThree()
        {
            var testGuid = Guid.NewGuid();
            var testScores = ScoreFactory.Create_ListOfScoreEntity(testGuid);
            var testRepo = new MockRepository<ScoreEntity>(testScores);
            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);

            var result = testClass.GetScoresByEvaluationId(testGuid);

            result.Count.ShouldBe(3);

        }

        [Fact]
        public void GetScoresByEvalId_NoScores_ReturnsZero()
        {
            var testGuid = Guid.NewGuid();
            var testRepo = new MockRepository<ScoreEntity>();
            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);

            var result = testClass.GetScoresByEvaluationId(testGuid);

            result.Count.ShouldBe(0);

        }

        [Fact]
        public void GetScoresByEvalId_DiffGuidExistingScores_ReturnsZero()
        {
            var testGuid = Guid.NewGuid();
            var testScores = ScoreFactory.Create_ListOfScoreEntity(testGuid);
            var testRepo = new MockRepository<ScoreEntity>(testScores);
            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);

            var result = testClass.GetScoresByEvaluationId(Guid.NewGuid());

            result.Count.ShouldBe(0);

        }

        [Fact]
        public void DeleteScore_ValidGuid_RemovesScore()
        {
            var evalGuid = Guid.NewGuid();
            var testList = ScoreFactory.Create_ListOfScoreEntity(evalGuid);
            var testRepo = new MockRepository<ScoreEntity>(testList);
            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);
            var testGuid = testRepo.GetAll().First().Id;

            testClass.DeleteScore(testGuid);

            var result = testClass.GetScoresByEvaluationId(evalGuid);

            result.Count.ShouldBe(2);
        }

        [Fact]
        public void DeleteScore_EmptyGuid_ThrowsObjectNotFound()
        {
            var testGuid = Guid.Empty;
            var testClass = InteractorFactory.Create_ScoreInteractor();

            Should.Throw<ObjectNotFoundException>(() => testClass.DeleteScore(testGuid));
        }

        [Fact]
        public void UpdateScore_ValidObject_UpdatesScore()
        {
            var evalGuid = Guid.NewGuid();
            var testList = ScoreFactory.Create_ListOfScoreEntity(evalGuid);
            var testRepo = new MockRepository<ScoreEntity>(testList);
            var testClass = InteractorFactory.Create_ScoreInteractor(testRepo);
            var scoreToUpdate = testRepo.GetAll().First();

            var updatedScore = new ScoreEntity {Id = scoreToUpdate.Id, PointsPossible = 5, PointsEarned = 4};

            testClass.UpdateScore(updatedScore);

            var result = testClass.GetScore(scoreToUpdate.Id);

            result.LastModified.ShouldNotBeSameAs(scoreToUpdate.LastModified);
            result.PointsEarned.ShouldBe(4);
            result.PointsPossible.ShouldBe(5);
        }

        [Fact]
        public void UpdateScore_NewObject_ThrowsObjectNotFound()
        {
            var testClass = InteractorFactory.Create_ScoreInteractor();

            var testScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();

            Should.Throw<ObjectNotFoundException>(() => testClass.UpdateScore(testScore));
        }
    }
}
