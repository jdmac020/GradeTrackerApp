using System;
using System.Collections.Generic;
using System.Linq;
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

        public Guid CreateCourse(CourseEntity newCourse)
        {
            var existingCourse = GetExistingRecord(newCourse);

            if (existingCourse != null)
                throw new ObjectAlreadyExistsException("A Course With the Same Department and Number Already Exists for this Semester.");

            ValidateNewCourse(newCourse);

            newCourse.Id = Guid.NewGuid();
            newCourse.LastModified = DateTime.Now;

            return Repo.Create(newCourse);
        }

        protected CourseEntity GetExistingRecord(CourseEntity newCourse)
        {
            return Repo.GetAll().FirstOrDefault(c => c.SemesterId.Equals(newCourse.SemesterId) &&
                                                                 c.Year.Equals(newCourse.Year) &&
                                                     c.Department.Equals(newCourse.Department) &&
                                                     c.Number.Equals(newCourse.Number));
        }

        protected void ValidateNewCourse(CourseEntity newCourse)
        {
            if (string.IsNullOrEmpty(newCourse.Name))
                throw new MissingInfoException("Course Must Have a Name.");
            if (string.IsNullOrEmpty(newCourse.Department))
                throw new MissingInfoException("Course Must Have a Department.");
            if (string.IsNullOrEmpty(newCourse.Number))
                throw new MissingInfoException("Course Must Have a Number.");
            if (newCourse.SemesterId.Equals(Guid.Empty))
                throw new MissingInfoException("Course Must Have a Semester.");
        }

        public CourseEntity GetCourse(Guid courseId)
        {
            if (courseId.Equals(Guid.Empty))
                throw new ObjectNotFoundException("Requested Course Does Not Exist.");

            return Repo.GetById(courseId);
        }

        public List<CourseEntity> GetAllCourses()
        {
            return Repo.GetAll().ToList();
        }

        public List<CourseEntity> GetCoursesByStudentId(Guid userId)
        {
            return Repo.GetAll().Where(c => c.StudentId.Equals(userId)).ToList();
        }
    }
}