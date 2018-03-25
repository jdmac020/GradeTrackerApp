using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Models;
using GradeTrackerApp.Models.Course;
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

        public ActionResult Add(Guid? guid)
        {
            throw new NotImplementedException();
        }

        public ActionResult ViewEvaluation(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}