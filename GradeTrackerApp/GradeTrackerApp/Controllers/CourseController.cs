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
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;
using GradeTrackerApp.Models.Evaluation;
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

        #endregion

        public CourseController() { }

        public CourseController(ICourseService mockCourseServicePositives, IEvaluationService mockEvaluationServiceEmpties, ISemesterService mockSemesterServiceFails)
        {
            _courseService = mockCourseServicePositives;
            _evaluationService = mockEvaluationServiceEmpties;
            _semesterService = mockSemesterServiceFails;
        }

        // GET: Course
        public ActionResult Index()
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
            var createModel = new CreateCourseViewModel();


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

        public ActionResult Create(CreateCourseViewModel createViewModel)
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

        protected EvaluationListViewModel ConvertToListViewModel(List<IDomainModel> domainModels)
        {
            var listOfViewModels = new List<EvaluationViewModel>();

            foreach (var eval in domainModels)
            {
                listOfViewModels.Add(new EvaluationViewModel((EvaluationDomainModel)eval));
            }

            return new EvaluationListViewModel(listOfViewModels);
        }

        protected CreateCourseDomainModel ConvertToDomainModel(CreateCourseViewModel viewModel)
        {
            return new CreateCourseDomainModel
            {
                StudentId = viewModel.StudentId,
                Name = viewModel.Name,
                Number = viewModel.Number,
                Department = viewModel.Department,
                Year = int.Parse(viewModel.Year),
                SemesterId = viewModel.SemesterId

            };
        }
    }
}