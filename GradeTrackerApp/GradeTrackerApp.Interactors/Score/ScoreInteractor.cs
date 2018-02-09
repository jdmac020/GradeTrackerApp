using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Score
{
    public class ScoreInteractor : IScoreInteractor 
    {
        public ScoreInteractor(IRepository<EvaluationEntity,Guid> mockRepo)
        {
            throw new NotImplementedException();
        }
    }
}