using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Scores.Service;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Score;

namespace GradeTrackerApp.Controllers
{
    public class ScoreController : BaseController
    {
        #region Services

        public IScoreService Scores
        {
            get { return _scoreService ?? (_scoreService = new ScoreService()); }
            set { _scoreService = value; }
        }

        private IScoreService _scoreService;

        public IEvaluationService Evaluations
        {
            get { return _evaluationService ?? (_evaluationService = new EvaluationService()); }
            set { _evaluationService = value; }
        }

        private IEvaluationService _evaluationService;

        #endregion

        public ActionResult StartUpdate(Guid scoreId)
        {
            var scoreDomainModel = Scores.GetScore(scoreId);

            if (ReferenceEquals(scoreDomainModel.GetType(), typeof(ErrorDomainModel)))
            {
                return GradeTrackerError(scoreDomainModel, null);
            }

            var viewModel = new ScoreViewModel((ScoreDomainModel)scoreDomainModel);

            return View("UpdateScore", viewModel);
        }

        public ActionResult UpdateScore(ScoreViewModel updatedScore)
        {
            if (ModelState.IsValid)
            {
                var domainModel = ConvertToDomainModel(updatedScore);

                var updatedModel = Scores.UpdateScore(domainModel);

                if (ReferenceEquals(updatedModel.GetType(), typeof(ErrorDomainModel)))
                {
                    return GradeTrackerError(updatedModel, updatedScore);
                }
                else
                {
                    var viewModel = new ScoreViewModel((ScoreDomainModel)updatedModel);

                    UpdateEvaluation(viewModel.EvaluationId);

                    

                    return View("ScoreUpdated", viewModel);
                }
            }
            else
            {
                return View("UpdateScore", updatedScore);
            }
        }

        private void UpdateEvaluation(Guid evaluationId)
        {
            // update grade
            Evaluations.ScoresUpdated(evaluationId);
        }

        public ActionResult DeleteScore(Guid scoreId)
        {
            var deletedScore = Scores.DeleteScore(scoreId);

            if (ReferenceEquals(deletedScore.GetType(), typeof(ErrorDomainModel)))
            {
                return GradeTrackerError(deletedScore, null);
            }
            else
            {
                var castedDomainModel = (ScoreDomainModel)deletedScore;

                var evaluationIdOnlyModel = new ScoreViewModel {EvaluationId = castedDomainModel.EvaluationId};

                Evaluations.ScoresUpdated(evaluationIdOnlyModel.EvaluationId);

                return View("ScoreDeleted", evaluationIdOnlyModel);
            }
        }

        public ActionResult ViewScore(Guid scoreId)
        {
            var domainModel = Scores.GetScore(scoreId);

            if (ReferenceEquals(domainModel.GetType(), typeof(ErrorDomainModel)))
            {
                var viewModel = new GradeTrackerErrorViewModel((ErrorDomainModel)domainModel);

                return View("GradeTrackerError", viewModel);
            }
            else
            {
                var viewModel = new ScoreViewModel((ScoreDomainModel)domainModel);

                return View(viewModel);
            }
        }

        public ActionResult AddScore(Guid evaluationId)
        {
            var createModel = new CreateOrEditScoreViewModel(evaluationId);

            return View(createModel);
        }

        public ActionResult RetryAddScore(CreateOrEditScoreViewModel retryModel)
        {
            return View("AddScore", retryModel);
        }

        public ActionResult CreateScore(CreateOrEditScoreViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var createDomainModel = ConvertToCreateDomainModel(createViewModel);

                var newScore = Scores.CreateNewScore(createDomainModel);

                if (ReferenceEquals(newScore.GetType(), typeof(ErrorDomainModel)))
                {
                    var errorViewModel = new GradeTrackerErrorViewModel((ErrorDomainModel) newScore)
                    {
                        ViewModel = createViewModel
                    };


                    return View("GradeTrackerError", errorViewModel);
                }
                else
                {
                    Evaluations.ScoresUpdated(createDomainModel.EvaluationId);

                    return Complete((ScoreDomainModel) newScore);
                }
            }
            else
            {
                return View("AddScore", createViewModel);
            }
        }

        public ActionResult Complete(ScoreDomainModel scoreModel)
        {
            var scoreViewModel = new ScoreViewModel(scoreModel);

            return View("ScoreAdded", scoreViewModel);
        }

        private static CreateOrEditScoreViewModel ConvertToCreateOrEditScoreViewModel(ScoreDomainModel domainModel)
        {
            return new CreateOrEditScoreViewModel
            {
                Id = domainModel.Id,
                EvaluationId = domainModel.EvaluationId,
                Name = domainModel.Name,
                Date = domainModel.Date,
                PointsEarned = domainModel.PointsEarned,
                PointsPossible = domainModel.PointsPossible
            };
        }

        private static ScoreDomainModel ConvertToDomainModel(ScoreViewModel createViewModel)
        {
            return new ScoreDomainModel
            {
                Id = createViewModel.Id,
                EvaluationId = createViewModel.EvaluationId,
                Name = createViewModel.Name,
                Date = createViewModel.Date,
                PointsEarned = createViewModel.PointsEarned,
                PointsPossible = createViewModel.PointsPossible,
            };
        }

        private static CreateScoreDomainModel ConvertToCreateDomainModel(CreateOrEditScoreViewModel createViewModel)
        {
            return new CreateScoreDomainModel
            {
                EvaluationId = createViewModel.EvaluationId,
                Name = createViewModel.Name,
                Date = createViewModel.Date.Value,
                PointsEarned = createViewModel.PointsEarned,
                PointsPossible = createViewModel.PointsPossible,
            };
        }
    }
}