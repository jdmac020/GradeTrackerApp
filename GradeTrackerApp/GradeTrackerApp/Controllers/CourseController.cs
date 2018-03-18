using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;

namespace GradeTrackerApp.Controllers
{
    public class CourseController : Controller
    {
        public ICourseService Courses
        {
            get { return _courseService ?? (_courseService = new CourseService()); }
            set { _courseService = value; }
        }

        private ICourseService _courseService;

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

            return View(createModel);
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