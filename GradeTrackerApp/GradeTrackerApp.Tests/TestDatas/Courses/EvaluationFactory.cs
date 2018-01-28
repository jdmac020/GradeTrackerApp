using GradeTrackerApp.Domain.Courses.Models;
using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class EvaluationFactory
    {
        public static CreateEvaluationDomainModel Create_CreateEvaluationDomainModel_ValidMinimum()
        {
            return new CreateEvaluationDomainModel
            {
                Name = "Tests",
                CourseId =  Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4"),
                WeightId = Guid.NewGuid(),
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
                WeightId = Guid.NewGuid(),
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
                CourseId = Guid.Parse("b59009e4-3f12-4eaf-a82c-bfaa6371b1a4"),
                WeightId = Guid.NewGuid(),
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
