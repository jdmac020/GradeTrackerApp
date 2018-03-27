using System;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Tests.TestDatas.Evaluations;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Evaluations
{
    public class ServiceTests
    {
        [Fact]
        public void CreateNewEvaluation_EmptyModel_ThrowsMissingInfoException()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testModel = new CreateEvaluationDomainModel();

            Should.Throw<MissingInfoException>(() => testClass.CreateNewEvaluation(testModel));
        }

        [Fact]
        public void CreateNewEvaluation_NegativeScoreCount_ThrowsMissingInfoException()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testModel = EvaluationFactory.Create_CreateEvaluationDomainModel_ValidMinimum();
            testModel.NumberOfScores = -3;

            Should.Throw<MissingInfoException>(() => testClass.CreateNewEvaluation(testModel));
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
        public void GetEvaluationById_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetEvaluation(testGuid));
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
        public void GetEvaluationsForCourse_EmptyGuid_ThrowsBadInfoException()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.Empty;

            Should.Throw<BadInfoException>(() => testClass.GetEvaluationsForCourse(testGuid));
        }

        [Fact]
        public void GetEvaluationsForCourse_ValidGuid_GetsTwoEvals()
        {
            var testClass = ServiceFactory.Create_EvaluationService();
            var testGuid = Guid.NewGuid();

            var result = testClass.GetEvaluationsForCourse(testGuid);

            result.Count.ShouldBe(2);
        }
    }
}
