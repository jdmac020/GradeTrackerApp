using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Scores;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Scores
{
    public class ServiceTests
    {
        [Fact]
        public void CreateNewScore_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = ServiceFactory.Create_ScoreService();
            var testModel = ScoreFactory.Create_CreateScoreDomainModel_Empty();

            Should.Throw<MissingInfoException>(() => testClass.CreateNewScore(testModel));
        }

        [Fact]
        public void CreateNewScore_ValidMin_ReturnsGoodGuid()
        {
            var testClass = ServiceFactory.Create_ScoreService();
            var testModel = ScoreFactory.Create_CreateScoreDomainModel_ValidMinimum();

            var result = testClass.CreateNewScore(testModel);

            result.Name.ShouldNotBe(string.Empty);
        }

        [Fact]
        public void GetScore_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = ServiceFactory.Create_ScoreService();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>((() => testClass.GetScore(testGuid)));
        }

        [Fact]
        public void GetScore_ValidGuid_GetsValidModel()
        {
            var testRepo = new MockRepository<ScoreEntity>();
            var testScore = ScoreFactory.Create_ScoreEntity_ValidMinimum();
            var testGuid = testRepo.Create(testScore);

            var testClass = ServiceFactory.Create_ScoreService();

            var result = testClass.GetScore(testGuid);

            result.Name.ShouldNotBe(string.Empty);

        }
    }
}
