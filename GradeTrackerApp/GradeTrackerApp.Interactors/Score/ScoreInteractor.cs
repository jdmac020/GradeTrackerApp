using System;
using System.Collections.Generic;
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

        public List<ScoreEntity> GetScoresByEvaluationId(Guid evaluationId)
        {
            return Repo.GetAll().Where(s => s.EvaluationId.Equals(evaluationId)).ToList();
        }

        public void DeleteScore(Guid scoreId)
        {
            var scoreToDelete = Repo.GetById(scoreId);

            if (scoreToDelete != null)
            {
                Repo.Delete(scoreToDelete);
            }
            else
            {
                throw new ObjectNotFoundException("There is no Score with that ID.");
            }

        }

        public void UpdateScore(ScoreEntity updatedScore)
        {
            var existingScore = Repo.GetById(updatedScore.Id);

            if (existingScore != null)
            {
                existingScore.PointsEarned = updatedScore.PointsEarned;
                existingScore.PointsPossible = updatedScore.PointsPossible;
                existingScore.PointsGrade = updatedScore.PointsGrade;
                existingScore.Date = updatedScore.Date;
                existingScore.Name = updatedScore.Name;
                existingScore.LastModified = DateTime.Now;

                Repo.Update(existingScore);
            }
            else
            {
                throw new ObjectNotFoundException("There is no Score with that ID.");
            }
        }

        public Guid CreateScore(ScoreEntity newScore)
        {
            var existingScore = GetExistingRecord(newScore.EvaluationId, newScore.Name);

            if (existingScore != null)
                throw new ObjectAlreadyExistsException("There is already a Score for that Evaluation with that Name.");

            ValidateNewScore(newScore);

            newScore.Id = Guid.NewGuid();

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