using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public class CourseService : ICourseService
    {
        private ICourseInteractor CourseInteractor
        {
            get { return _createInteractor ?? (_createInteractor = new CourseInteractor()); }
            set { _createInteractor = value; }
        }

        private ICourseInteractor _createInteractor;

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="createInteractor">Interactor to be used by class</param>
        public CourseService(ICourseInteractor createInteractor)
        {
            _createInteractor = createInteractor;
        }

        public CourseService()
        {

        }

        public CourseDomainModel CreateNewCourse(CreateCourseDomainModel createModel)
        {
            var newCourseEntity = ConvertModelToEntity(createModel);

            var courseEntity = CourseInteractor.CreateCourse(newCourseEntity);

            var courseModel = new CourseDomainModel(courseEntity);

            // set school
            // set instructor
            // set evaluations
            // set set grade thresholds
            // calculate points/grades

            return courseModel;

        }

        private CourseEntity ConvertModelToEntity(CreateCourseDomainModel createModel)
        {
            throw new System.NotImplementedException();
        }
    }
}