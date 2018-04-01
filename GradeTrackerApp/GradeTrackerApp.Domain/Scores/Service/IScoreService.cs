using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Scores.Service
{
    public interface IScoreService
    {
        IDomainModel CreateNewScore(CreateScoreDomainModel createModel);
        IDomainModel GetScore(Guid scoreId);
        List<IDomainModel> GetScoresForEvaluation(Guid evaluationId);
    }
}