using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Scores.Service
{
    public interface IScoreService
    {
        IDomainModel DeleteScore(Guid scoreId);
        IDomainModel UpdateScore(ScoreDomainModel updatedScoreModel);
        IDomainModel CreateNewScore(CreateScoreDomainModel createModel);
        IDomainModel GetScore(Guid scoreId);
        List<IDomainModel> GetScoresForEvaluation(Guid evaluationId);
    }
}