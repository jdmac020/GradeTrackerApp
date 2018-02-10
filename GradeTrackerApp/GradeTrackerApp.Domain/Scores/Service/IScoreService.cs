using System;
using GradeTrackerApp.Domain.Scores.Models;

namespace GradeTrackerApp.Domain.Scores.Service
{
    public interface IScoreService
    {
        IDomainModel CreateNewScore(CreateScoreDomainModel createModel);
        IDomainModel GetScore(Guid evaluationId);
    }
}