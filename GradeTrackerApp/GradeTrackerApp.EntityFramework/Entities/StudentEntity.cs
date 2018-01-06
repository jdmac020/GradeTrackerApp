using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class StudentEntity : IdentityUser, IEntity<string>
    {
        // Id = a username, prolly should set up to be the e-mail address
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid AccessLevelId { get; set; }
        public virtual List<SchoolEntity> Schools { get; set; }
        public virtual List<CourseEntity> AllCourses { get; set; } 
        public virtual List<CourseEntity> ActiveCourses { get; set; }
    }
}