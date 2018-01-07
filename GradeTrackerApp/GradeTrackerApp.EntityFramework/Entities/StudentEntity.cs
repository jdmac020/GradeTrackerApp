using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class StudentEntity : IdentityUser, IEntity<string>
    {
        //Id = a username, prolly should set up to be the e-mail address
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid AccessLevelId { get; set; }
        public virtual List<SchoolEntity> Schools { get; set; }
        public virtual List<CourseEntity> AllCourses { get; set; } 
        public virtual List<CourseEntity> ActiveCourses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<StudentEntity> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}