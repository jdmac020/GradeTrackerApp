using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Core.Models;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Course
{
    public class CreateCourse : ICreateCourse
    {
        private IRepository<CourseEntity, Guid> Repo
        {
            get { return _courseRepository ?? ( _courseRepository = new Repository<CourseEntity, Guid>()); }
            set { _courseRepository = value; }
        }

        private IRepository<CourseEntity, Guid> _courseRepository;

        /// <summary>
        /// Constructor to override default repository
        /// </summary>
        /// <param name="repo">Repository to be used by class</param>
        public CreateCourse(IRepository<CourseEntity, Guid> repo)
        {
            _courseRepository = repo;
        }

        public CreateCourse() { }

        public CourseEntity Execute(CreateCourseDomainModel domainModel)
        {
            var courseToAdd = new CourseEntity();
            var addedCourse = new CourseEntity();

            try
            {
                courseToAdd = ConvertModel(domainModel);
                addedCourse = Repo.Create(courseToAdd);
            }
            catch (InvalidOperationException e)
            {
                throw new MissingInfoException();
            }
            catch (NullReferenceException e)
            {
                throw new MissingInfoException();
            }

            return addedCourse;
        }

        private CourseEntity ConvertModel(CreateCourseDomainModel domainModel)
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
                CurrentPointsGrade = 0,
                FinalPointsGrade = 0,
                CreatedOn = DateTime.Now,
            };
        }
    }
}
