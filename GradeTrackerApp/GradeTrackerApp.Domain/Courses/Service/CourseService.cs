using GradeTrackerApp.Core.Models;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public class CourseService : ICourseService
    {
        private ICreateCourse CreateInteractor
        {
            get { return _createInteractor ?? (_createInteractor = new CreateCourse()); }
            set { _createInteractor = value; }
        }

        private ICreateCourse _createInteractor;

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="createInteractor">Interactor to be used by class</param>
        public CourseService(ICreateCourse createInteractor)
        {
            _createInteractor = createInteractor;
        }

        public CourseService()
        {
            
        }

        public CourseDomainModel CreateNewCourse(CreateCourseDomainModel createModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
