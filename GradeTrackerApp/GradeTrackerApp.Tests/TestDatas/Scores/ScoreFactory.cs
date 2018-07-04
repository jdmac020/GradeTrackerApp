using System;
using System.Collections.Generic;
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
                PointsEarned = 0
            };
        }

        public static CreateScoreDomainModel Create_CreateScoreDomainModel_80Percent()
        {
            return new CreateScoreDomainModel
            {
                Name = "Quiz",
                EvaluationId = Guid.Empty,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 10,
                PointsEarned = 8
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
                PointsPossible = 10,
                PointsEarned = 8,
                PointsGrade = .8

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
                PointsPossible = 10,
                PointsEarned = 8,
                PointsGrade = .8

            };
        }

        public static ScoreEntity Create_ScoreEntity_ValidMinimum(Guid scoreId, string scoreName, Guid evalId)
        {
            return new ScoreEntity
            {
                Id = scoreId,
                Name = scoreName,
                EvaluationId = evalId,
                Date = DateTime.Parse("1/23/2018"),
                PointsPossible = 10,
                PointsEarned = 8,
                PointsGrade = .8

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

        public static List<ScoreEntity> Create_ListOfScoreEntity(Guid evaluationId)
        {
            var list = new List<ScoreEntity>();

            list.Add(Create_ScoreEntity_ValidMinimum(Guid.NewGuid(), "Test 1", evaluationId));
            list.Add(Create_ScoreEntity_ValidMinimum(Guid.NewGuid(), "Test 2", evaluationId));
            list.Add(Create_ScoreEntity_ValidMinimum(Guid.NewGuid(), "Test 3", evaluationId));

            return list;
        }
    }
}
