﻿using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Interactors.Score;
using GradeTrackerApp.Tests.TestDatas.Scores;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockScoreInteractor : IScoreInteractor
    {
        public void DeleteScore(Guid scoreId)
        {
            if (scoreId.Equals(Guid.Empty))
                throw new ObjectNotFoundException("The Score Requested Does Not Exist.");
            
        }

        public void UpdateScore(ScoreEntity updatedScore)
        {
            if (updatedScore.Id.Equals(Guid.Empty))
                throw new ObjectNotFoundException("The Score Requested Does Not Exist.");
        }

        public Guid CreateScore(ScoreEntity domainModel)
        {
            if (string.IsNullOrEmpty(domainModel.Name))
            {
                throw new MissingInfoException();
            }
            else
            {
                return Guid.NewGuid();
            }
        }

        public ScoreEntity GetScore(Guid scoreId)
        {
            if (scoreId.Equals(Guid.Empty))
                throw new ObjectNotFoundException();

            return ScoreFactory.Create_ScoreEntity_ValidMinimum(scoreId);
        }

        public List<ScoreEntity> GetScoresByEvaluationId(Guid evaluationId)
        {
            return ScoreFactory.Create_ListOfScoreEntity(evaluationId);
        }
    }
}