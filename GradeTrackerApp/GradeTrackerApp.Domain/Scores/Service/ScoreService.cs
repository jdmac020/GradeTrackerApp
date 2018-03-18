﻿using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Scores.Models;
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
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
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
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
            }

            return scoreModel;
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