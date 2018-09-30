using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Tests.TestDatas.Evaluations
{
    public static class EvaluationFactory
    {

        private static Guid _courseId = Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4");

        public static List<EvaluationEntity> Create_ListValidEvalEntities()
        {
            return new List<EvaluationEntity>
            {
                Create_EvaluationEntity_ValidMinimum_CustomId(Guid.NewGuid()),
                Create_EvaluationEntity_ValidMinimum_CustomId(Guid.NewGuid()),
                Create_EvaluationEntity_ValidMinimum_CustomId(Guid.NewGuid())
            };
        }

        public static CreateEvaluationDomainModel Create_CreateEvaluationDomainModel_ValidMinimum()
        {
            return new CreateEvaluationDomainModel
            {
                Name = "Tests",
                CourseId = _courseId,
                Weight = 1,
                NumberOfScores = 3,
                DropLowest = false
            };
        }

        public static EvaluationEntity Create_EvaluationEntity_ValidMinimum()
        {
            return new EvaluationEntity
            {
                Name = "Tests",
                CourseId = _courseId,
                Weight = 1,
                NumberOfScores = 3,
                DropLowest = false
            };
        }

        public static EvaluationEntity Create_EvaluationEntity_ValidMinimum_CustomId(Guid evalId)
        {
            return new EvaluationEntity
            {
                Id = evalId,
                Name = "Tests",
                CourseId = _courseId,
                Weight = 1,
                NumberOfScores = 3,
                DropLowest = false
            };
        }

        public static EvaluationEntity Create_EvaluationEntity_ValidMinimum(Guid courseId)
        {
            return new EvaluationEntity
            {
                Id = courseId,
                Name = "Tests",
                CourseId = courseId,
                Weight = 1,
                NumberOfScores = 3,
                DropLowest = false
            };
        }

        public static CreateOrEditCourseDomainModel Create_CreateEvaluationDomainModel_Empty()
        {
            return new CreateOrEditCourseDomainModel();
        }


        /// <summary>
        /// Returns 4 Id-only models
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<EvaluationDomainModel> Create_ListOfDomainModels()
        {
            return new List<EvaluationDomainModel>
            {
                new EvaluationDomainModel { CourseId = _courseId, Id = Guid.NewGuid() },
                new EvaluationDomainModel { CourseId = _courseId, Id = Guid.NewGuid() },
                new EvaluationDomainModel { CourseId = _courseId, Id = Guid.NewGuid() },
                new EvaluationDomainModel { CourseId = _courseId, Id = Guid.NewGuid() }
            };
        }
    }
}
