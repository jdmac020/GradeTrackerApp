using System;
using System.Collections.Generic;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Evaluations;
using Shouldly;
using Xunit;

namespace GradeTrackerApp.Tests.Evaluations
{
    public class InteractorTests
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
            result.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void CreateEval_DuplicateName_ThrowsObjectAlreadyExists()
        {
            var testRepo = new MockRepository<EvaluationEntity>();
            var existingEval = EvaluationFactory.Create_EvaluationEntity_ValidMinimum_CustomId(Guid.NewGuid());
            testRepo.Update(existingEval);

            var testEval = EvaluationFactory.Create_EvaluationEntity_ValidMinimum();

            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            Should.Throw<ObjectAlreadyExistsException>(() => testClass.CreateEvaluation(testEval));
        }

        [Fact]
        public void GetById_EmptyGuid_ThrowsObjectNotFound()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<ObjectNotFoundException>(() => testClass.GetEvaluation(testGuid));

        }

        [Fact]
        public void GetById_ValidGuid_ReturnsTestEntity()
        {
            var testRepo = new Mocks.MockRepository<EvaluationEntity>();
            var testEntity = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(Guid.NewGuid());
            testRepo.Update(testEntity);
            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            var result = testClass.GetEvaluation(testEntity.Id);

            result.ShouldBe(testEntity);

        }

        [Fact]
        public void GetByCourseId_EmptyGuid_ThrowsBadInfoException()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();
            var testGuid = Guid.Empty;

            Should.Throw<BadInfoException>(() => testClass.GetEvaluationsByCourseId(testGuid));
        }

        [Fact]
        public void GetByCourseId_ValidCourseId_ReturnsTwoEvals()
        {
            var testRepo = new MockRepository<EvaluationEntity>();
            var testGuid = Guid.NewGuid();
            var testEvalOne = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(testGuid);
            var testEvalTwo = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(testGuid);

            testRepo.Create(testEvalOne);
            testRepo.Create(testEvalTwo);

            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            var result = testClass.GetEvaluationsByCourseId(testGuid);

            result.Count.ShouldBe(2);
        }

        [Fact]
        public void GetByCourseId_2ValidCourseIds_ReturnsOneEvals()
        {
            var testRepo = new MockRepository<EvaluationEntity>();
            var testGuid1 = Guid.NewGuid();
            var testGuid2 = Guid.NewGuid();
            var testEvalOne = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(testGuid1);
            var testEvalTwo = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(testGuid2);

            testRepo.Create(testEvalOne);
            testRepo.Create(testEvalTwo);

            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            var result = testClass.GetEvaluationsByCourseId(testGuid1);

            result.Count.ShouldBe(1);
        }

        [Fact]
        public void DeleteEvaluation_ValidGuid_RemovesEvaluation()
        {
            var deleteEvalId = Guid.NewGuid();
            var remainEvalId = Guid.NewGuid();
            var courseId = Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4");
            var testList =
                new List<EvaluationEntity>
                {
                    EvaluationFactory.Create_EvaluationEntity_ValidMinimum_CustomId(deleteEvalId),
                    EvaluationFactory.Create_EvaluationEntity_ValidMinimum_CustomId(remainEvalId)

                };

            var testRepo = new MockRepository<EvaluationEntity>(testList);
            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);

            testClass.DeleteEvaluation(deleteEvalId);

            var result = testClass.GetEvaluationsByCourseId(courseId);

            result.Count.ShouldBe(1);
        }

        [Fact]
        public void DeleteEvaluation_EmptyGuid_ThrowsObjectNotFound()
        {
            var testGuid = Guid.Empty;
            var testClass = InteractorFactory.Create_EvaluationInteractor();

            Should.Throw<ObjectNotFoundException>(() => testClass.DeleteEvaluation(testGuid));
        }

        [Fact]
        public void UpdateEvaluation_ValidObject_UpdatesScore()
        {
            var evalGuid = Guid.NewGuid();
            var testList = EvaluationFactory.Create_ListValidEvalEntities();
            var testRepo = new MockRepository<EvaluationEntity>(testList);
            var testClass = InteractorFactory.Create_EvaluationInteractor(testRepo);
            var evaluationToUpdate = testRepo.GetAll().First();

            var updatedEvaluation = new EvaluationEntity { Id = evaluationToUpdate.Id, Weight = .2, DropLowest = true, NumberOfScores = 1};

            testClass.UpdateEvaluation(updatedEvaluation);

            var result = testClass.GetEvaluation(evaluationToUpdate.Id);

            result.LastModified.ShouldNotBeSameAs(evaluationToUpdate.LastModified);
            result.Weight.ShouldBe(.2);
            result.DropLowest.ShouldBe(true);
            result.NumberOfScores.ShouldBe(1);
        }

        [Fact]
        public void UpdateEvaluation_NewObject_ThrowsObjectNotFound()
        {
            var testClass = InteractorFactory.Create_EvaluationInteractor();

            var testScore = EvaluationFactory.Create_EvaluationEntity_ValidMinimum();

            Should.Throw<ObjectNotFoundException>(() => testClass.UpdateEvaluation(testScore));
        }
    }
}
