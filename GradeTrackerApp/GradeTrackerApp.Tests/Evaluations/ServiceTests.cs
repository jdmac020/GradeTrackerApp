using System;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Evaluations;
using GradeTrackerApp.Tests.TestDatas.Scores;
using Shouldly;
using Xunit;
using ServiceFactory = GradeTrackerApp.Tests.TestDatas.Evaluations.ServiceFactory;

namespace GradeTrackerApp.Tests.Evaluations
{
    public class ServiceTests
    {
        [Fact]
        public void CreateNewEvaluation_EmptyModel_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testModel = new CreateEvaluationDomainModel();

            var result = testClass.CreateNewEvaluation(testModel);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void CreateNewEvaluation_NegativeScoreCount_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testModel = EvaluationFactory.Create_CreateEvaluationDomainModel_ValidMinimum();
            testModel.NumberOfScores = -3;

            var result = testClass.CreateNewEvaluation(testModel);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void CreateNewEvaluation_ValidModel_ResultNotNull()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testModel = EvaluationFactory.Create_CreateEvaluationDomainModel_ValidMinimum();

            var result = testClass.CreateNewEvaluation(testModel);

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetEvaluationById_EmptyGuid_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.Empty;

            var result = testClass.GetEvaluation(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetEvaluationById_NewGuid_ReturnsEntityWithMatchingGuid()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.NewGuid();

            var result = (EvaluationDomainModel)testClass.GetEvaluation(testGuid);

            result.Id.ShouldBe(testGuid);
        }

        [Fact]
        public void GetEvaluationsForCourse_EmptyGuid_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.Empty;

            var result = testClass.GetEvaluationsForCourse(testGuid);

            result.First().GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetEvaluationsForCourse_ValidGuid_GetsTwoEvals()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.NewGuid();

            var result = testClass.GetEvaluationsForCourse(testGuid);

            result.Count.ShouldBe(2);
        }

        [Fact]
        public void DeleteEvaluation_EmptyGuid_ReturnsErrorModel()
        {
            var testGuid = Guid.Empty;
            var testClass = ServiceFactory.Create_EvaluationService();

            var result = testClass.DeleteEvaluation(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void DeleteEvaluation_ValidGuid_DeletesObject()
        {
            var evaluationId = Guid.NewGuid();

            var testClass = ServiceFactory.Create_EvaluationService();
            testClass.EvaluationInteractor = TestDatas.Evaluations.InteractorFactory.Create_MockEvaluationInteractor();
            testClass.ScoreInteractor = TestDatas.Scores.InteractorFactory.Create_MockScoreInteractor();
            
            var result = testClass.DeleteEvaluation(evaluationId);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateEvaluation_EmptyGuid_ReturnsErrorModel()
        {
            var testScore = new EvaluationDomainModel { Id = Guid.Empty };
            var testClass = ServiceFactory.Create_EvaluationService();

            var result = testClass.UpdateEvaluation(testScore);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateEvaluation_ValidModel_ReturnsValidModel()
        {
            var testEvaluationEntity = EvaluationFactory.Create_EvaluationEntity_ValidMinimum_CustomId(Guid.NewGuid());
            var testEvaluation = new EvaluationDomainModel(testEvaluationEntity);
            var testClass = ServiceFactory.Create_EvaluationService();

            var result = testClass.UpdateEvaluation(testEvaluation);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void DeleteEvaluation_ScoresPresent_DeletesScores()
        {
            var testEvalId = Guid.NewGuid();
            var testEval = EvaluationFactory.Create_EvaluationEntity_ValidMinimum_CustomId(testEvalId);
            var testDomainModel = new EvaluationDomainModel(testEval);
            var testScores = ScoreFactory.Create_ListOfScoreEntity(testEvalId);
            var testScoreRepo = new MockRepository<ScoreEntity>(testScores);
            var scoreInteractor = TestDatas.Scores.InteractorFactory.Create_ScoreInteractor();
            scoreInteractor.Repo = testScoreRepo;
            var mockEvalInteractor = TestDatas.Evaluations.InteractorFactory.Create_MockEvaluationInteractor();
            var testClass = ServiceFactory.Create_EvaluationService();
            testClass.EvaluationInteractor = mockEvalInteractor;
            testClass.ScoreInteractor = scoreInteractor;

            var result = testClass.DeleteEvaluation(testEvalId);
            var resultScores = testClass.ScoreInteractor.GetScoresByEvaluationId(testEvalId);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
            resultScores.Count.ShouldBe(0);
        }
    }
}
