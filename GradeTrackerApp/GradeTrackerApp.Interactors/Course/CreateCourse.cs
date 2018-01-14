using System;
using GradeTrackerApp.Domain.Models.Courses;
using GradeTrackerApp.EntityFramework.Entities;
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
        public CourseEntity Execute(CreateCourseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
