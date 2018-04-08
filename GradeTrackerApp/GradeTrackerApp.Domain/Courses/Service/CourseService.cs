using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Shared;
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

        public IDomainModel CreateCourse(CreateCourseDomainModel createModel)
        {
            var newCourseEntity = ConvertModelToEntity(createModel);

            var courseId = Guid.Empty;
            var courseModel = new CourseDomainModel();

            try
            {
                courseId = CourseInteractor.CreateCourse(newCourseEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            try
            {
                courseModel = (CourseDomainModel)GetCourse(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }
            
            return courseModel;

        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IDomainModel GetCourse(Guid courseId)
        {
            var courseEntity = new CourseEntity();

            try
            {
                courseEntity = CourseInteractor.GetCourse(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            var courseModel = new CourseDomainModel(courseEntity);

            return courseModel;
        }

        // Will eventually pass in a student identifier and get all the courses associated
        public List<IDomainModel> GetCourses(Guid userId)
        {
            var domainModels = new List<IDomainModel>();
            var courseEntities = new List<CourseEntity>();

            try
            {
                courseEntities = CourseInteractor.GetCoursesByStudentId(userId);
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> {new ErrorDomainModel(gte, false)};
            }

            foreach (var entity in courseEntities)
            {
                domainModels.Add(new CourseDomainModel(entity));
            }

            return domainModels;
        }



        private CourseEntity ConvertModelToEntity(CreateCourseDomainModel createModel)
        {
            return new CourseEntity
            {
                StudentId = createModel.StudentId,
                Name = createModel.Name,
                Department = createModel.Department,
                Number = createModel.Number,
                //SchoolId = createModel.SchoolId,
                //InstructorId = createModel.InstructorId,
                Year = createModel.Year,
                SemesterId = createModel.SemesterId,
                //StartTime = createModel.StartTime,
                //EndTime = createModel.EndTime,
                //StartDate = createModel.StartDate,
                //EndDate = createModel.EndDate
            };
        }
    }
}