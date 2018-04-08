using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Domain.Scores.Service;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;
using GradeTrackerApp.Models.Evaluation;
using GradeTrackerApp.Models.Score;
using GradeTrackerApp.Models.Semester;
using Microsoft.AspNet.Identity;

namespace GradeTrackerApp.Controllers
{
    public class EvaluationController : Controller
    {
        #region Services

        public ICourseService Courses
        {
            get { return _courseService ?? (_courseService = new CourseService()); }
            set { _courseService = value; }
        }

        private ICourseService _courseService;

        public IEvaluationService Evaluations
        {
            get { return _evaluationService ?? (_evaluationService = new EvaluationService()); }
            set { _evaluationService = value; }
        }

        private IEvaluationService _evaluationService;

        public IScoreService Scores
        {
            get { return _scoreService ?? (_scoreService = new ScoreService()); }
            set { _scoreService = value; }
        }

        private IScoreService _scoreService;

        #endregion
        
        public ActionResult Complete(EvaluationDomainModel evaluationModel)
        {
            var evaluationViewModel = new EvaluationViewModel(evaluationModel);

            return View("EvaluationComplete", evaluationViewModel);
        }

        protected CreateEvaluationDomainModel ConvertToDomainModel(CreateEvaluationViewModel viewModel)
        {
            return new CreateEvaluationDomainModel
            {
                CourseId = viewModel.CourseId,
                Name = viewModel.Name,
                Weight = viewModel.Weight,
                NumberOfScores = viewModel.NumberOfScores,
                DropLowest = viewModel.DropLowest
            };
        }

        public ActionResult Add(Guid courseId)
        {
            var createModel = new CreateEvaluationViewModel(courseId);

            return View(createModel);
        }

        public ActionResult ViewEvaluation(Guid evaluationId)
        {
            var evaluationDomainModel = Evaluations.GetEvaluation(evaluationId);
            var evaluationViewModel = new EvaluationViewModel((EvaluationDomainModel)evaluationDomainModel);
            evaluationViewModel.Scores = GetScoresForEvaluation(evaluationId);

            return View(evaluationViewModel);
        }

        private ScoreListViewModel GetScoresForEvaluation(Guid evaluationId)
        {
            var listOfDomainModels = Scores.GetScoresForEvaluation(evaluationId);

            if (listOfDomainModels.GetType() == typeof(List<ErrorDomainModel>))
            {
                return new ScoreListViewModel();
            }
            else
            {
                var listViewModel = GetListViewModelFromDomainModels(listOfDomainModels);

                return listViewModel;
            }
        }

        private ScoreListViewModel GetListViewModelFromDomainModels(IEnumerable<IDomainModel> listOfDomainModels)
        {
            var listOfViewModels = new List<ScoreViewModel>();

            foreach (var domainModel in listOfDomainModels)
            {
                listOfViewModels.Add(new ScoreViewModel((ScoreDomainModel)domainModel));
            }

            return new ScoreListViewModel(listOfViewModels);
        }

        public ActionResult Create(CreateEvaluationViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var createDomainModel = ConvertToDomainModel(viewModel);

                var newDomainModel = Evaluations.CreateNewEvaluation(createDomainModel);

                if (newDomainModel.GetType() == typeof(EvaluationDomainModel))
                {
                    return Complete((EvaluationDomainModel)newDomainModel);
                }
                else
                {
                    var errorModel = new GradeTrackerErrorViewModel((ErrorDomainModel)newDomainModel);

                    return View("GradeTrackerError", errorModel);
                }

                
            }
            else
            {
                return View("Add", viewModel);
            }

            
        }
    }
}