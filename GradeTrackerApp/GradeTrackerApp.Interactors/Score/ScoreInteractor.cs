using System;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Score
{
    public class ScoreInteractor : IScoreInteractor
    {
        public IRepository<ScoreEntity, Guid> Repo
        {
            get { return _repo ?? (_repo = new Repository<ScoreEntity, Guid>()); }
            set { _repo = value; }
        }

        private IRepository<ScoreEntity, Guid> _repo;

        public ScoreInteractor() { }

        public ScoreInteractor(IRepository<ScoreEntity, Guid> mockRepo)
        {
            _repo = mockRepo;
        }

        public ScoreEntity GetScore(Guid scoreId)
        {
            if (scoreId.Equals(Guid.Empty))
                throw new ObjectNotFoundException("The Score Requested Does Not Exist.");

            var foundScore = Repo.GetById(scoreId);

            return foundScore;
        }

        public Guid CreateScore(ScoreEntity newScore)
        {
            var existingScore = GetExistingRecord(newScore.EvaluationId, newScore.Name);

            if (existingScore != null)
                throw new ObjectAlreadyExistsException("There is already a Score for that Evaluation with that Name.");

            ValidateNewScore(newScore);

            return Repo.Create(newScore);
        }

        protected ScoreEntity GetExistingRecord(Guid evaluationId, string scoreName)
        {
            return  Repo.GetAll().FirstOrDefault(s => s.EvaluationId.Equals(evaluationId) && s.Name.Equals(scoreName));
        }

        protected void ValidateNewScore(ScoreEntity newScore)
        {
            if (newScore.PointsPossible < 0)
                throw new BadInfoException("Minimum Value for Points Possible is Zero.");

            if (newScore.PointsEarned < 0)
                throw new BadInfoException("Minimum Value for Points Earned is Zero.");

            if (string.IsNullOrEmpty(newScore.Name))
                throw new MissingInfoException("All Scores Must Have a Unique Name.");

            var existingScore = Repo
                .GetAll()
                .FirstOrDefault(s => s.EvaluationId.Equals(newScore.EvaluationId) && s.Name.Equals(newScore.Name));

            if (existingScore != null)
                throw new ObjectAlreadyExistsException("There is already a Score for that Evaluation with that Name.");

        }
    }
}