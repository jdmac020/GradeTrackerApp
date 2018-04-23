using System;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Evaluations;
using GradeTrackerApp.Tests.TestDatas.Scores;
using Shouldly;
using Xunit;
using ServiceFactory = GradeTrackerApp.Tests.TestDatas.Scores.ServiceFactory;

namespace GradeTrackerApp.Tests.Scores
{
    public class ServiceTests
    {
        [Fact]
        public void CreateNewScore_EmptyModel_ReturnsErrorModel()
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
        public void GetScore_EmptyGuid_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_ScoreService();
            var testGuid = Guid.Empty;

            var result = testClass.GetScore(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
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

        [Fact]
        public void DeleteScore_EmptyGuid_ReturnsErrorModel()
        {
            var testGuid = Guid.Empty;
            var testClass = ServiceFactory.Create_ScoreService();

            var result = testClass.DeleteScore(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void DeleteScore_ValidGuid_DeletesObject()
        {
            var evaluation = Guid.NewGuid();

            var testClass = ServiceFactory.Create_ScoreService();

            var result = testClass.DeleteScore(evaluation);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateScore_EmptyGuid_ReturnsErrorModel()
        {
            var testScore = new ScoreDomainModel {Id = Guid.Empty};
            var testClass = ServiceFactory.Create_ScoreService();

            var result = testClass.UpdateScore(testScore);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateScore_ValidModel_ReturnsValidModel()
        {
            var testScoreEntity = ScoreFactory.Create_ScoreEntity_ValidMinimum(Guid.NewGuid());
            var testScore = new ScoreDomainModel(testScoreEntity);
            var testClass = ServiceFactory.Create_ScoreService();

            var result = testClass.UpdateScore(testScore);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }
    }
}
