using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Course
{
    public class CourseInteractor : ICourseInteractor
    {
        private IRepository<CourseEntity, Guid> Repo
        {
            get { return _courseRepository ?? (_courseRepository = new Repository<CourseEntity, Guid>()); }
            set { _courseRepository = value; }
        }

        private IRepository<CourseEntity, Guid> _courseRepository;

        /// <summary>
        /// Constructor to override default repository
        /// </summary>
        /// <param name="repo">Repository to be used by class</param>
        public CourseInteractor(IRepository<CourseEntity, Guid> repo)
        {
            _courseRepository = repo;
        }

        public CourseInteractor() { }

        public Guid CreateCourse(CourseEntity domainModel)
        {
            var courseToAdd = new CourseEntity();
            var addedCourseId = Guid.Empty;

            try
            {
                courseToAdd = ConvertModelToNewEntity(domainModel);
                addedCourseId = Repo.Create(courseToAdd);
            }
            catch (InvalidOperationException e)
            {
                throw new MissingInfoException("There was missing data that prevented creation. See Inner Exception for details.", e);
            }
            catch (NullReferenceException e)
            {
                throw new MissingInfoException("There was missing data that prevented creation. See Inner Exception for details.", e);
            }

            return addedCourseId;
        }

        public CourseEntity GetCourseById(Guid courseId)
        {
            if (courseId.Equals(Guid.Empty))
                throw new MissingInfoException();

            return Repo.GetById(courseId);
        }

        private CourseEntity ConvertModelToNewEntity(CourseEntity domainModel)
        {
            return new CourseEntity
            {
                Id = Guid.NewGuid(),
                Name = domainModel.Name,
                Department = domainModel.Department,
                Number = domainModel.Number,
                SchoolId = domainModel.SchoolId,
                InstructorId = domainModel.InstructorId,
                Year = domainModel.Year,
                SemesterId = domainModel.SemesterId,
                EndTime = domainModel.EndTime,
                StartTime = domainModel.StartTime,
                StartDate = domainModel.StartDate,
                EndDate = domainModel.EndDate,
                TotalPointsPossible = 0,
                CurrentPointsPossible = 0,
                PointsEarned = 0,
                EvaluationCount = domainModel.EvaluationCount,
                CurrentPointsGrade = 100,
                FinalPointsGrade = 100,
                CurrentLetterGrade = "A",
                FinalLetterGrade = "A",
                CreatedOn = DateTime.Now,
            };
        }
    }
}