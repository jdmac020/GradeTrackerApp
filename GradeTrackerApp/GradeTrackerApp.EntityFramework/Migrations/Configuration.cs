using GradeTrackerApp.EntityFramework.Entities;

namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.GradeTrackerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GradeTrackerApp.EntityFramework.Contexts.GradeTrackerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.AccessLevels.AddOrUpdate(x => x.Id,
                new AccessLevelEntity {Id = Guid.NewGuid(), Name = "Basic"},
                new AccessLevelEntity { Id = Guid.NewGuid(), Name = "Enhanced"},
                new AccessLevelEntity { Id = Guid.NewGuid(), Name = "Admin"}
                );

            context.Semesters.AddOrUpdate(x => x.Id,
                new SemesterEntity { Id = Guid.NewGuid(), Name = "Fall"},
                new SemesterEntity { Id = Guid.NewGuid(), Name = "Spring"},
                new SemesterEntity { Id = Guid.NewGuid(), Name = "Summer"}
                );

            // add school type
            
            // add test school

            // add test student

            // add test instructor
            
        }
    }
}
