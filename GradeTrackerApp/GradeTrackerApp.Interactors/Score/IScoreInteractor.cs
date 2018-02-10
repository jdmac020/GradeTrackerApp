using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Score
{
    public interface IScoreInteractor
    {
        Guid CreateScore(ScoreEntity newScoreEntity);
        ScoreEntity GetScore(Guid scoreId);
    }
}