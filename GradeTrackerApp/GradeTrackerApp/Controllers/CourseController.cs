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
using Microsoft.AspNet.Identity;

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
            // maybe we get a model for this based on the student defaults?
            // will need a model to assign values...

            var createModel = new CreateCourseViewModel();
            
            createModel.Semesters = GetSemestersForDropDown();

            return View(createModel);
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

                var newCourseViewModel = new CourseViewModel(domainModel);

                newCourseViewModel.Semester = (SemesterDomainModel)Semesters.GetSemester(domainModel.SemesterId);

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

            courseViewModel.Semester = (SemesterDomainModel)Semesters.GetSemester(courseDomainModel.SemesterId);

            return View(courseViewModel);
        }

        protected CreateCourseDomainModel ConvertToDomainModel(CreateCourseViewModel viewModel)
        {
            return new CreateCourseDomainModel
            {
                StudentId = viewModel.StudentId,
                Name = viewModel.Name,
                Number = viewModel.Number,
                Department = viewModel.Department,
                Year = viewModel.Year,
                SemesterId = viewModel.SemesterId

            };
        }
    }
}