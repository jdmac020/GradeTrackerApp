using System;
using System.Web.Mvc;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Scores.Service;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Score;

namespace GradeTrackerApp.Controllers
{
    public class ScoreController : Controller
    {
        #region Services

        public IScoreService Scores
        {
            get { return _scoreService ?? (_scoreService = new ScoreService()); }
            set { _scoreService = value; }
        }

        private IScoreService _scoreService;

        #endregion

        public ActionResult ViewScore(Guid scoreId)
        {
            var domainModel = Scores.GetScore(scoreId);

            if (domainModel.GetType().Equals(typeof(ErrorDomainModel)))
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
            var createModel = new CreateScoreViewModel(evaluationId);

            return View(createModel);
        }

        public ActionResult RetryAddScore(CreateScoreViewModel retryModel)
        {
            return View("AddScore", retryModel);
        }

        public ActionResult CreateScore(CreateScoreViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var createDomainModel = ConvertToDomainModel(createViewModel);

                var newScore = Scores.CreateNewScore(createDomainModel);

                if (newScore.GetType().Equals(typeof(ErrorDomainModel)))
                {
                    var viewModel = new GradeTrackerErrorViewModel((ErrorDomainModel)newScore);

                    viewModel.ViewModel = createViewModel;
                    
                    return View("GradeTrackerError", viewModel);
                }
                else
                {
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

        private static CreateScoreDomainModel ConvertToDomainModel(CreateScoreViewModel createViewModel)
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