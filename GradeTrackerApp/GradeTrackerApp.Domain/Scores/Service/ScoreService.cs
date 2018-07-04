using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Score;
using ConjureGrade.Wizards;

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

        public IDomainModel DeleteScore(Guid scoreId)
        {
            var deletedScoreModel = new ScoreDomainModel();

            try
            {
                var score = Interactor.GetScore(scoreId);

                Interactor.DeleteScore(scoreId);

                deletedScoreModel = new ScoreDomainModel {EvaluationId = score.EvaluationId};

            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            return deletedScoreModel;
        }

        public IDomainModel UpdateScore(ScoreDomainModel updatedScoreModel)
        {
            var returnModel = new ScoreDomainModel();

            try
            {
                var entityToUpdate = ConvertModelToEntity(updatedScoreModel);

                CalculateGrade(entityToUpdate);

                Interactor.UpdateScore(entityToUpdate);

                var updatedEntity = Interactor.GetScore(entityToUpdate.Id);

                returnModel = new ScoreDomainModel(updatedEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            return returnModel;
        }

        public IDomainModel CreateNewScore(CreateScoreDomainModel createModel)
        {
            var scoreModel = new ScoreDomainModel();
            var newScoreEntity = ConvertModelToEntity(createModel);

            try
            {
                CalculateGrade(newScoreEntity);

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
            var entities = new List<ScoreEntity>();
            var models = new List<IDomainModel>();

            try
            {
                entities = Interactor.GetScoresByEvaluationId(evaluationId);
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> {new ErrorDomainModel(gte, false)};
            } 
            

            foreach (var entity in entities)
            {
                models.Add(new ScoreDomainModel(entity));
            }

            return models;
        }

        private void CalculateGrade(ScoreEntity scoreToCalculate)
        {
            var wizard = new ScoreWizard();

            var result = wizard.GetSingleScoreResult(scoreToCalculate.PointsEarned, scoreToCalculate.PointsPossible);

            scoreToCalculate.PointsGrade = result.GradeRaw;
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

        private static ScoreEntity ConvertModelToEntity(ScoreDomainModel createModel)
        {
            return new ScoreEntity
            {
                Id = createModel.Id,
                Name = createModel.Name,
                EvaluationId = createModel.EvaluationId,
                Date = createModel.Date,
                PointsPossible = createModel.PointsPossible,
                PointsEarned = createModel.PointsEarned
            };
        }
    }
}