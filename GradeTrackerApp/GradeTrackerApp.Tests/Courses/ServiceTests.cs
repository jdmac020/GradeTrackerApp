﻿using System;
using System.Collections.Generic;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.Mocks;
using GradeTrackerApp.Tests.TestDatas.Courses;
using GradeTrackerApp.Tests.TestDatas.Evaluations;
using Shouldly;
using Xunit;
using ServiceFactory = GradeTrackerApp.Tests.TestDatas.Courses.ServiceFactory;

namespace GradeTrackerApp.Tests.Courses
{
    public class ServiceTests
    {
        [Fact]
        public void CreateCourse_EmptyModel_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testModel = new CreateOrEditCourseDomainModel();

            var result = testClass.CreateCourse(testModel);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void CreateCourse_ValidModel_ResultNotNull()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testModel = CourseFactory.Create_CreateCourseDomainModel_ValidMinimum();

            var result = testClass.CreateCourse(testModel);

            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetCourse_EmptyGuid_ReturnsErrorModel()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var testGuid = Guid.Empty;

            var result = testClass.GetCourse(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetCourse_ValidGuid_NameNotEmpty()
        {
            var testGuid = Guid.NewGuid();

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourse(testGuid);

            result.Name.ShouldNotBe(string.Empty);
        }

        [Fact]
        public void GetCourses_EmptyGuid_ThrowsBadInfoException()
        {
            var testGuid = Guid.Empty;

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourses(testGuid);

            result.First().GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void GetCourses_NewGuid_ReturnsTwoDomainModels()
        {
            var testGuid = Guid.NewGuid();

            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.GetCourses(testGuid);
        }

        [Fact]
        public void DeleteCourse_EmptyGuid_ReturnsErrorModel()
        {
            var testGuid = Guid.Empty;
            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.DeleteCourse(testGuid);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void DeleteCourse_ValidGuid_DeletesObject()
        {
            var courseId = Guid.NewGuid();

            var testClass = ServiceFactory.Create_MockInteractor();
            //testClass.EvaluationService = TestDatas.Evaluations.ServiceFactory.Create_EvaluationService();

            var result = testClass.DeleteCourse(courseId);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateCourse_EmptyGuid_ReturnsErrorModel()
        {
            var testCourse = new CreateOrEditCourseDomainModel { Id = Guid.Empty };
            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.UpdateCourse(testCourse);

            result.GetType().ShouldBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void UpdateCourse_ValidModel_ReturnsValidModel()
        {
            var testCourseId = Guid.NewGuid();
            var testCourseEntity = CourseFactory.Create_CourseEntity_ValidMinimum(testCourseId);
            var testCourseModel = new CreateOrEditCourseDomainModel(testCourseEntity);
            var testClass = ServiceFactory.Create_MockInteractor();

            var result = testClass.UpdateCourse(testCourseModel);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
        }

        [Fact]
        public void DeleteCourse_EvalsPresent_DeletesEvals()
        {
            var testCourseId = Guid.NewGuid();
            var testEvals = new List<EvaluationEntity>
            {
                EvaluationFactory.Create_EvaluationEntity_ValidMinimum(testCourseId)
            };

            var testEvalRepo = new MockRepository<EvaluationEntity>(testEvals);
            var evalService = TestDatas.Evaluations.ServiceFactory.Create_EvaluationService();
            var evalInteractor = TestDatas.Evaluations.InteractorFactory.Create_EvaluationInteractor();
            evalInteractor.Repo = testEvalRepo;
            evalService.EvaluationInteractor = evalInteractor;
            evalService.ScoreInteractor = new MockScoreInteractor();
            var testClass = ServiceFactory.Create_MockInteractor();
            testClass.EvaluationService = evalService;

            var result = testClass.DeleteCourse(testCourseId);
            var resultScores = testClass.EvaluationService.GetEvaluationsForCourse(testCourseId);

            result.GetType().ShouldNotBe(typeof(ErrorDomainModel));
            resultScores.Count.ShouldBe(0);
        }

        [Fact]
        public void CalcWhatIfGrade_InputsOf100_Return100()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var evals = EvaluationFactory.Create_ListOfDomainModels();

            evals = evals.Select(e => new EvaluationDomainModel { Id = e.Id, CourseId = e.CourseId, Weight = 1, PointsEarned = 100, TotalPointsPossible = 100 });

            var result = testClass.CalcWhatIfGrade(evals);

            result.WhatIfGrade.ShouldBe(100);
        }

        [Fact]
        public void CalcWhatIfGrade_WeightedInputsOf100_Return100()
        {
            var testClass = ServiceFactory.Create_MockInteractor();
            var evals = EvaluationFactory.Create_ListOfDomainModels().ToList();

            evals[0].TotalPointsPossible = 25;
            evals[0].PointsEarned = 25;
            evals[0].Weight = .5;
            evals[1].TotalPointsPossible = 25;
            evals[1].PointsEarned = 25;
            evals[1].Weight = .15;
            evals[2].TotalPointsPossible = 25;
            evals[2].PointsEarned = 25;
            evals[2].Weight = .15;
            evals[3].TotalPointsPossible = 25;
            evals[3].PointsEarned = 25;
            evals[3].Weight = .20;

            var result = testClass.CalcWhatIfGrade(evals);

            result.WhatIfGrade.ShouldBe(100);
        }
    }
}
