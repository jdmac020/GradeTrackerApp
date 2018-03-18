﻿using System;
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
            var existingEval = EvaluationFactory.Create_EvaluationEntity_ValidMinimum(Guid.NewGuid());
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
    }
}