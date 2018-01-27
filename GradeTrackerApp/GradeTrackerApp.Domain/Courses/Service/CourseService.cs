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

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public CourseDomainModel GetCourse(Guid courseId)
        {
            var courseEntity = CourseInteractor.GetCourseById(courseId);

            var courseModel = new CourseDomainModel(courseEntity);

            return courseModel;
        }



        private CourseEntity ConvertModelToEntity(CreateCourseDomainModel createModel)
        {
            return new CourseEntity
            {
                Name = createModel.Name,
                Department = createModel.Department,
                Number = createModel.Number,
                SchoolId = createModel.SchoolId,
                InstructorId = createModel.InstructorId,
                Year = createModel.Year,
                SemesterId = createModel.SemesterId,
                StartTime = createModel.StartTime,
                EndTime = createModel.EndTime,
                StartDate = createModel.StartDate,
                EndDate = createModel.EndDate
            };
        }
    }
}