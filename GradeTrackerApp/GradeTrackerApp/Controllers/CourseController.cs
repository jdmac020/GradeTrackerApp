using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;
using GradeTrackerApp.Models.Semester;

namespace GradeTrackerApp.Controllers
{
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

        #endregion

        // GET: Course
        public ActionResult Index()
        {
            var courses = Courses.GetCourses();

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
            // maybe we get a model for this based on the student defaults?
            // will need a model to assign values...

            var createModel = new CreateCourseViewModel();

            var semesterModels = Semesters.GetAllSemesters();

            createModel.Semesters = ConvertSemesterDomainModels(semesterModels);

            return View(createModel);
        }

        protected List<SelectListItem> ConvertSemesterDomainModels(List<IDomainModel> models)
        {
            var modelList = new List<SelectListItem>();

            foreach (var domainModel in models)
            {
                var semesterModel = (SemesterDomainModel)domainModel;

                modelList.Add(new SelectListItem {Value = semesterModel.Id.ToString(), Text = semesterModel.Name});
            }

            return modelList;
        }

        public ActionResult Create(CreateCourseViewModel createViewModel)
        {
            var createModel = ConvertToDomainModel(createViewModel);

            var domainModel = Courses.CreateCourse(createModel);

            var newCourseViewModel = new CourseViewModel(domainModel);

            return ViewCourse(newCourseViewModel);
        }

        public ActionResult ViewCourse(CourseViewModel model)
        {
            return View(model);
        }

        protected CreateCourseDomainModel ConvertToDomainModel(CreateCourseViewModel viewModel)
        {
            return new CreateCourseDomainModel
            {
                Name = viewModel.Name,
                Number = viewModel.Number,
                Department = viewModel.Department,
                SchoolId = viewModel.SchoolId,
                InstructorId = viewModel.InstructorId,
                Year = viewModel.Year,
                SemesterId = viewModel.SemesterId,
                StartDate = viewModel.StartDate,
                StartTime = viewModel.StartTime,
                EndDate = viewModel.EndDate,
                EndTime = viewModel.EndTime,

            };
        }
    }
}