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
    [Authorize]
    public class CourseController : BaseController
    {
        #region Services

        public ICourseService Courses
        {
            get { return _courseService ?? (_courseService = new CourseService()); }
            set { _courseService = value; }
        }

        private ICourseService _courseService;

        public ISemesterService Semesters
        {
            get { return _semesterService ?? (_semesterService = new SemesterService()); }
            set { _semesterService = value; }
        }

        private ISemesterService _semesterService;

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

        public CourseController() { }

        public CourseController(ICourseService mockCourseServicePositives, IEvaluationService mockEvaluationServiceEmpties, ISemesterService mockSemesterServiceFails)
        {
            _courseService = mockCourseServicePositives;
            _evaluationService = mockEvaluationServiceEmpties;
            _semesterService = mockSemesterServiceFails;
        }

        // GET: Course
        public ActionResult AllCourses()
        {
            var userId = User.Identity.GetUserId();
            
            var courses = Courses.GetCourses(Guid.Parse(userId));

            var courseViewModels = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                courseViewModels.Add(new CourseViewModel((CourseDomainModel)course));
            }

            var model = new CourseListViewModel(courseViewModels);

            return View(model);
        }

        public ActionResult Add()
        {
            var createModel = new CreateOrEditCourseViewModel();
            
            var semesterModels = Semesters.GetAllSemesters();

            if (semesterModels.Count > 0 && semesterModels.First().GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(semesterModels.First(), null);
            }

            createModel.Semesters = GetSemestersForDropDown(semesterModels);
            createModel.YearOptions = GetYearDropDownOptions();

            return View(createModel);
        }

        protected List<SelectListItem> GetYearDropDownOptions()
        {
            var today = DateTime.Today;
            var dropDownList = new List<SelectListItem>();

            var thisYear = today.Year.ToString();
            var nextYear = today.AddYears(1).Year.ToString();

            dropDownList.Add(new SelectListItem {Text = thisYear, Value = thisYear});
            dropDownList.Add(new SelectListItem {Text = nextYear, Value = nextYear});

            return dropDownList;
        }

        protected List<SelectListItem> GetSemestersForDropDown(List<IDomainModel> semesterModels)
        {  
            var modelList = new List<SelectListItem>();

            foreach (var semester in semesterModels)
            {
                var semesterModel = (SemesterDomainModel)semester;

                modelList.Add(new SelectListItem {Value = semesterModel.Id.ToString(), Text = semesterModel.Name});
            }

            return modelList;
        }

        protected SemesterViewModel GetSemesterViewModel(IDomainModel semesterDomainModel)
        {
            return new SemesterViewModel((SemesterDomainModel)semesterDomainModel);
        }

        public ActionResult Create(CreateOrEditCourseViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var createModel = ConvertToDomainModel(createViewModel);

                createModel.StudentId = Guid.Parse(userId);

                var domainIModel = Courses.CreateCourse(createModel);
                var domainModel = new CourseDomainModel();

                if (domainIModel.GetType() == typeof(ErrorDomainModel))
                {
                    var semesterModels = Semesters.GetAllSemesters();

                    if (semesterModels.Count > 0 && semesterModels.First().GetType() == typeof(ErrorDomainModel))
                    {
                        return GradeTrackerError(semesterModels.First(), null);
                    }

                    createViewModel.Semesters = GetSemestersForDropDown(semesterModels);
                    createViewModel.YearOptions = GetYearDropDownOptions();

                    return GradeTrackerError(domainIModel, createViewModel);
                }
                else
                {
                    domainModel = (CourseDomainModel)domainIModel;
                }
                
                var newCourseViewModel = new CourseViewModel(domainModel);

                var semesterModel = Semesters.GetSemester(domainModel.SemesterId);

                if (semesterModel.GetType() == typeof(ErrorDomainModel))
                {
                    return GradeTrackerError(semesterModel, null);
                }

                newCourseViewModel.Semester = GetSemesterViewModel(semesterModel);

                return View("ViewCourse",newCourseViewModel);
            }
            else
            {
                var semesterModels = Semesters.GetAllSemesters();

                if (semesterModels.Count > 0 && semesterModels.First().GetType() == typeof(ErrorDomainModel))
                {
                    return GradeTrackerError(semesterModels.First(), null);
                }

                createViewModel.Semesters = GetSemestersForDropDown(semesterModels);
                createViewModel.YearOptions = GetYearDropDownOptions();

                return View("Add", createViewModel);
            }
            
        }

        public ActionResult ViewCourse(Guid courseId)
        {
            var courseDomainModel = new CourseDomainModel();
            var iModel = Courses.GetCourse(courseId);

            if (iModel.GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(iModel, null);
            }
            else
            {
                courseDomainModel = (CourseDomainModel) iModel;
            }

            var courseViewModel = new CourseViewModel(courseDomainModel);

            var evaluationDomainModels = Evaluations.GetEvaluationsForCourse(courseId);

            if (evaluationDomainModels.Count > 0 && evaluationDomainModels.First().GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(evaluationDomainModels.First(), null);
            }

            var semesterModel = Semesters.GetSemester(courseDomainModel.SemesterId);

            if (semesterModel.GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(semesterModel, null);
            }

            courseViewModel.Semester = GetSemesterViewModel(semesterModel);
            courseViewModel.Evaluations = ConvertToListViewModel(evaluationDomainModels);
            courseViewModel.SetLastModified();

            return View(courseViewModel);
        }

        protected List<EvaluationViewModel> ConvertToListOfViewModels(List<IDomainModel> domainModels)
        {
            var listOfViewModels = new List<EvaluationViewModel>();

            foreach (var eval in domainModels)
            {
                listOfViewModels.Add(new EvaluationViewModel((EvaluationDomainModel)eval));
            }

            return listOfViewModels;
        }

        protected EvaluationListViewModel ConvertToListViewModel(List<IDomainModel> domainModels)
        {
            var listOfViewModels = new List<EvaluationViewModel>();

            foreach (var eval in domainModels)
            {
                listOfViewModels.Add(new EvaluationViewModel((EvaluationDomainModel)eval));
            }

            return new EvaluationListViewModel(listOfViewModels);
        }

        protected CourseDomainModel ConvertToDomainModel(CourseViewModel viewModel)
        {
            return new CourseDomainModel
            {
                Id = (Guid)viewModel.Id,
                StudentId = viewModel.StudentId,
                Name = viewModel.Name,
                Number = viewModel.Number,
                Department = viewModel.Department,
                Year = viewModel.Year,
                SemesterId = viewModel.SemesterId

            };
        }

        protected CreateOrEditCourseDomainModel ConvertToDomainModel(CreateOrEditCourseViewModel viewModel)
        {
            return new CreateOrEditCourseDomainModel
            {
                Id = viewModel.Id,
                StudentId = viewModel.StudentId,
                Name = viewModel.Name,
                Number = viewModel.Number,
                Department = viewModel.Department,
                Year = int.Parse(viewModel.Year),
                SemesterId = viewModel.SemesterId

            };
        }

        public ActionResult EditCourse(Guid courseid)
        {
            var domainModel = Courses.GetCourse(courseid);

            if (domainModel.GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(domainModel, null);
            }

            var viewModel = new CreateOrEditCourseViewModel((CourseDomainModel)domainModel);
            var semesters = Semesters.GetAllSemesters();
            viewModel.Semesters = GetSemestersForDropDown(semesters);
            viewModel.YearOptions = GetYearDropDownOptions();

            return View("UpdateCourse", viewModel);
        }

        public ActionResult UpdateCourse(CreateOrEditCourseViewModel updatedCourse)
        {
            if (ModelState.IsValid)
            {
                var domainModel = ConvertToDomainModel(updatedCourse);

                var updatedModel = Courses.UpdateCourse(domainModel);

                if (ReferenceEquals(updatedModel.GetType(), typeof(ErrorDomainModel)))
                {
                    return GradeTrackerError(updatedModel, updatedCourse);
                }
                else
                {
                    var castedDomainModel = (CourseDomainModel)updatedModel;
                    var viewModel = new CourseViewModel((CourseDomainModel)updatedModel);
                    var semesterViewModel = Semesters.GetSemester(castedDomainModel.SemesterId);
                    viewModel.Semester = GetSemesterViewModel(semesterViewModel);

                    return View("CourseUpdated", viewModel);
                }
            }
            else
            {
                return View("UpdateCourse", updatedCourse);
            }
        }

        public ActionResult DeleteCourse(Guid courseId)
        {
            var deletedCourse = Courses.DeleteCourse(courseId);

            if (ReferenceEquals(deletedCourse.GetType(), typeof(ErrorDomainModel)))
            {
                return GradeTrackerError(deletedCourse, null);
            }
            else
            {
                var castedDomainModel = (CourseDomainModel)deletedCourse;

                var courseIdOnlyModel = new CourseViewModel { StudentId = castedDomainModel.StudentId };

                return View("CourseDeleted", courseIdOnlyModel);
            }
        }

        
        public ActionResult GetWhatIfGrade(CourseWhatIfViewModel whatIfModel)
        {
            return View();
        }

        public ActionResult StartWhatIfGrade(Guid courseId)
        {
            var courseDomainModel = new CourseDomainModel();
            var iModel = Courses.GetCourse(courseId);

            if (iModel.GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(iModel, null);
            }
            else
            {
                courseDomainModel = (CourseDomainModel)iModel;
            }

            var courseWhatIfViewModel = new CourseWhatIfViewModel { Id = courseDomainModel.Id, Name = courseDomainModel.Name };

            var evaluationDomainModels = Evaluations.GetEvaluationsForCourse(courseId);

            if (evaluationDomainModels.Count > 0 && evaluationDomainModels.First().GetType() == typeof(ErrorDomainModel))
            {
                return GradeTrackerError(evaluationDomainModels.First(), null);
            }
            
            courseWhatIfViewModel.EvaluationList = ConvertToListOfViewModels(evaluationDomainModels);

            var scoresList = new List<ScoreViewModel>();

            foreach (var eval in courseWhatIfViewModel.EvaluationList)
            {
                var scoresDomainModel = Scores.GetScoresForEvaluation(eval.Id);

                if (courseWhatIfViewModel.ScoreList is null)
                {
                    courseWhatIfViewModel.ScoreList = EvaluationController.GetListViewModelFromDomainModels(scoresDomainModel);
                }
                else
                {
                    courseWhatIfViewModel.ScoreList.AddRange(EvaluationController.GetListViewModelFromDomainModels(scoresDomainModel));
                }

                
            }

            return View("WhatIfEntryView", courseWhatIfViewModel);
        }
    }
}