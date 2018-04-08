using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Score;

namespace GradeTrackerApp.Domain.Scores.Service
{
    public class ScoreService : IScoreService
    {
        #region Interactors

        public IScoreInteractor Interactor
        {
            get { return _interactor ?? (_interactor = new ScoreInteractor());}
            set { _interactor = value; }
        }

        private IScoreInteractor _interactor;

        #endregion

        public ScoreService() { }

        public ScoreService(IScoreInteractor interactor)
        {
            _interactor = interactor;
        }

        public IDomainModel CreateNewScore(CreateScoreDomainModel createModel)
        {
            var scoreModel = new ScoreDomainModel();
            var newScoreEntity = ConvertModelToEntity(createModel);

            try
            {
                var scoreId = Interactor.CreateScore(newScoreEntity);

                scoreModel = (ScoreDomainModel)GetScore(scoreId);
            }
            catch (ObjectAlreadyExistsException oae)
            {
                var errorModel = new ErrorDomainModel(oae, true);

                return errorModel;
            }

            return scoreModel;
        }

        public IDomainModel GetScore(Guid scoreId)
        {
            var scoreModel = new ScoreDomainModel();

            try
            {
                var scoreEntity = Interactor.GetScore(scoreId);

                scoreModel = new ScoreDomainModel(scoreEntity);
            }
            catch (GradeTrackerException e)
            {
                var errorModel = new ErrorDomainModel(e, false);

                return errorModel;
            }

            return scoreModel;
        }

        public List<IDomainModel> GetScoresForEvaluation(Guid evaluationId)
        {
            var entities = Interactor.GetScoresByEvaluationId(evaluationId);

            var models = new List<IDomainModel>();

            foreach (var entity in entities)
            {
                models.Add(new ScoreDomainModel(entity));
            }

            return models;
        }

        private static ScoreEntity ConvertModelToEntity(CreateScoreDomainModel createModel)
        {
            return new ScoreEntity
            {
                Name = createModel.Name,
                EvaluationId = createModel.EvaluationId,
                Date = createModel.Date,
                PointsPossible = createModel.PointsPossible,
                PointsEarned = createModel.PointsEarned
            };
        }
    }
}