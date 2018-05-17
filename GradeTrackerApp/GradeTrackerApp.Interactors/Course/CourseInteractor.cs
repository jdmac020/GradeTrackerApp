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
        public IRepository<CourseEntity, Guid> Repo
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
            newCourse.CreatedOn = DateTime.Now;
            newCourse.LastModified = DateTime.Now;
            newCourse.IsActive = true;

            return Repo.Create(newCourse);
        }

        public void DeleteCourse(Guid courseId)
        {
            var courseToDelete = Repo.GetById(courseId);

            if (courseToDelete != null)
            {
                Repo.Delete(courseToDelete);
            }
            else
            {
                throw new ObjectNotFoundException("There is no Score with that ID.");
            }
        }

        public void UpdateCourse(CourseEntity updatedCourse)
        {
            var existingCourse = Repo.GetById(updatedCourse.Id);

            if (existingCourse != null)
            {
                existingCourse.Number = updatedCourse.Number;
                existingCourse.Department = updatedCourse.Department;
                existingCourse.SemesterId = updatedCourse.SemesterId;
                existingCourse.Year = updatedCourse.Year;
                existingCourse.Name = updatedCourse.Name;
                existingCourse.LastModified = DateTime.Now;

                Repo.Update(existingCourse);
            }
            else
            {
                throw new ObjectNotFoundException("There is no Course with that ID.");
            }
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
            if (userId.Equals(Guid.Empty))
                throw new BadInfoException("There is no valid StudentId attached to this Request.");

            return Repo.GetAll().Where(c => c.StudentId.Equals(userId) && c.IsActive.Equals(true)).ToList();
        }

        public void UpdateLastModified(Guid courseId)
        {
            var existingCourse = Repo.GetById(courseId);

            if (existingCourse != null)
            {
                existingCourse.LastModified = DateTime.Now;

                Repo.Update(existingCourse);
            }
            else
            {
                throw new ObjectNotFoundException("There is no Course with that ID.");
            }
        }
    }
}