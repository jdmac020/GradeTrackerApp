using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Tests.TestDatas.Evaluations
{
    public static class EvaluationFactory
    {
        
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
                CourseId =  Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4"),
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
                CourseId = Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4"),
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
                CourseId = Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4"),
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

        public static CreateCourseDomainModel Create_CreateEvaluationDomainModel_Empty()
        {
            return new CreateCourseDomainModel();
        }
    }
}
