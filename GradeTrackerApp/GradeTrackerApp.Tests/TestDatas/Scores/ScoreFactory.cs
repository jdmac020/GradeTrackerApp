using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Scores.Models;

namespace GradeTrackerApp.Tests.TestDatas.Scores
{
    public static class ScoreFactory
    {
        public static CreateScoreDomainModel Create_CreateScoreDomainModel_Empty()
        {
            return new CreateScoreDomainModel();
        }

        public static CreateScoreDomainModel Create_CreateScoreDomainModel_ValidMinimum()
        {
            return new CreateScoreDomainModel
            {
                Name = "Quiz",
                EvaluationId = Guid.Empty,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 0,
                PointsEarned = 0,
                PointsGrade = 0
            };
        }

        public static ScoreEntity Create_ScoreEntity_Empty()
        {
            return new ScoreEntity();
        }

        public static ScoreEntity Create_ScoreEntity_ValidMinimum()
        {
            return new ScoreEntity
            {
                Name = "Quiz",
                EvaluationId = Guid.Empty,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 0,
                PointsEarned = 0,
                PointsGrade = 0

            };
        }
        
        public static ScoreEntity Create_ScoreEntity_ValidMinimum(Guid scoreId)
        {
            return new ScoreEntity
            {
                Id = scoreId,
                Name = "Quiz",
                EvaluationId = Guid.Empty,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 0,
                PointsEarned = 0,
                PointsGrade = 0

            };
        }

        public static ScoreEntity Create_ScoreEntity_ValidMinimum(double pointsPossible, double pointsEarned)
        {
            return new ScoreEntity
            {
                Name = "Quiz",
                EvaluationId = Guid.Empty,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 0,
                PointsEarned = 0,
                PointsGrade = 0

            };
        }

    }
}
