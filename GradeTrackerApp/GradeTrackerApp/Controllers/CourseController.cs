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
    [Authorize]
    public class CourseController : Controller
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

        // GET: Course
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            
            var courses = Courses.GetCourses(Guid.Parse(userId));

            var courseViewModels = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                courseViewModels.Add(new CourseViewModel(course));
            }

            var model = new CourseListViewModel(courseViewModels);

            return View(model);
        }

        public ActionResult Add()
        {
            var createModel = new CreateCourseViewModel();
            
            createModel.Semesters = GetSemestersForDropDown();
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

        protected List<SelectListItem> GetSemestersForDropDown()
        {
            var semesterModels = Semesters.GetAllSemesters();

            var modelList = new List<SelectListItem>();

            foreach (var semester in semesterModels)
            {
                var semesterModel = (SemesterDomainModel)semester;

                modelList.Add(new SelectListItem {Value = semesterModel.Id.ToString(), Text = semesterModel.Name});
            }

            return modelList;
        }

        public ActionResult Create(CreateCourseViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var createModel = ConvertToDomainModel(createViewModel);

                createModel.StudentId = Guid.Parse(userId);

                var domainModel = Courses.CreateCourse(createModel);

                var semesterDomainModel = (SemesterDomainModel)Semesters.GetSemester(domainModel.SemesterId);

                var newCourseViewModel = new CourseViewModel(domainModel);

                newCourseViewModel.Semester = new SemesterViewModel(semesterDomainModel);

                return View("ViewCourse",newCourseViewModel);
            }
            else
            {
                createViewModel.Semesters = GetSemestersForDropDown();

                return View("Add", createViewModel);
            }
            
        }

        public ActionResult ViewCourse(Guid courseId)
        {
            var courseDomainModel = Courses.GetCourse(courseId);
            var courseViewModel = new CourseViewModel(courseDomainModel);

            var evaluationDomainModels = Evaluations.GetEvaluationsForCourse(courseId);
            var semester = (SemesterDomainModel) Semesters.GetSemester(courseDomainModel.SemesterId);

            courseViewModel.Semester = new SemesterViewModel(semester);
            courseViewModel.Evaluations = ConvertToListViewModel(evaluationDomainModels);
            courseViewModel.SetLastModified();

            return View(courseViewModel);
        }

        protected EvaluationListViewModel ConvertToListViewModel(List<EvaluationDomainModel> domainModels)
        {
            var listOfViewModels = new List<EvaluationViewModel>();

            foreach (var eval in domainModels)
            {
                listOfViewModels.Add(new EvaluationViewModel(eval));
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