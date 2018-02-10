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
            get { return _courseInteractor ?? (_courseInteractor = new CourseInteractor()); }
            set { _courseInteractor = value; }
        }

        private ICourseInteractor _courseInteractor;

        #endregion

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="courseInteractor">Interactor to be used by class</param>
        public CourseService(ICourseInteractor courseInteractor)
        {
            _courseInteractor = courseInteractor;
        }
        
        public CourseService()
        {

        }

        public CourseDomainModel CreateNewCourse(CreateCourseDomainModel createModel)
        {
            var courseModel = new CourseDomainModel();

            var newCourseEntity = ConvertModelToEntity(createModel);

            try
            {
                var courseId = CourseInteractor.CreateCourse(newCourseEntity);

                courseModel = GetCourse(courseId);
            }
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
            }
            
            return courseModel;

        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public CourseDomainModel GetCourse(Guid courseId)
        {
            var courseEntity = new CourseEntity();

            try
            {
                courseEntity = CourseInteractor.GetCourseById(courseId);
            }
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
            }
            
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