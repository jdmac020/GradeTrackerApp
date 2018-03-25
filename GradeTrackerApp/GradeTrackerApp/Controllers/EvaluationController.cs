using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;
using GradeTrackerApp.Models.Evaluation;
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

        #endregion

        // GET: Course
        public ActionResult Index(Guid courseId)
        {
            var courses = Courses.GetCourses(courseId);

            var courseViewModels = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                courseViewModels.Add(new CourseViewModel(course));
            }

            var model = new CourseListViewModel(courseViewModels);

            return View(model);
        }

        public ActionResult Complete(EvaluationDomainModel evaluationModel)
        {
            var evaluationViewModel = new EvaluationViewModel(evaluationModel);

            return PartialView("_evaluationComplete", evaluationViewModel);
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
            throw new NotImplementedException();
        }

        public ActionResult Create(CreateEvaluationViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var createDomainModel = ConvertToDomainModel(viewModel);

                var newDomainModel = Evaluations.CreateNewEvaluation(createDomainModel);

                if (newDomainModel.GetType().Equals(typeof(EvaluationDomainModel)))
                {
                    return Complete((EvaluationDomainModel)newDomainModel);
                }

                return View();
            }
            else
            {
                return View("Add", viewModel);
            }

            
        }
    }
}