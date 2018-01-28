using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.TestDatas.Courses;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Evaluations
{
    public class EvaluationInteractorTests
    {
        [Fact]
        public void CreateEval_EmptyEntity_ThrowsMissingInfoException()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();
            var testEntity = new EvaluationEntity();

            Should.Throw<MissingInfoException>(() => testClass.CreateEvaluation(testEntity));
        }

        [Fact]
        public void CreateEval_ValidModel_ResultNotNull()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();
            var testModel = EvaluationFactory.Create_EvaluationEntity_ValidMinimum();

            var result = testClass.CreateEvaluation(testModel);

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetById_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetEvaluationById(testGuid));

        }

        [Fact]
        public void GetById_ValidGuid_ReturnsTestEntity()
        {
            var testRepo = new Mocks.MockRepository<EvaluationEntity, Guid>();
            var testEntity = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(Guid.NewGuid());
            testRepo.Update(testEntity);
            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            var result = testClass.GetEvaluationById(testEntity.Id);

            result.ShouldBe(testEntity);

        }
    }
}
