using System;
using System.Collections.Generic;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class StudentEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AccessLevelId { get; set; }
        public virtual List<SchoolEntity> Schools { get; set; }
        public virtual List<CourseEntity> AllCourses { get; set; } 
        public virtual List<CourseEntity> ActiveCourses { get; set; }
    }
}