using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public class CourseService : ICourseService
    {
        #region Services



        #endregion

        #region Interactors

        
        private ICourseInteractor CourseInteractor
        {
            get { return _createInteractor ?? (_createInteractor = new CourseInteractor()); }
            set { _createInteractor = value; }
        }

        private ICourseInteractor _createInteractor;

        #endregion

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

            var courseId = CourseInteractor.CreateCourse(newCourseEntity);

            var courseModel = GetCourse(courseId);

            return courseModel;

        }

        public CourseDomainModel GetCourse(Guid courseId)
        {
            var courseEntity = CourseInteractor.GetCourseById(courseId);

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