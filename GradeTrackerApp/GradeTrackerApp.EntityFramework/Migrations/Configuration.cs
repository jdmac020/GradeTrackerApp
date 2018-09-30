using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GradeTrackerApp.EntityFramework.Contexts.GradeTrackerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(GradeTrackerApp.EntityFramework.Contexts.GradeTrackerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.AccessLevels.Count().Equals(0))
            {
                context.AccessLevels.AddOrUpdate(x => x.Id,
                    new AccessLevelEntity { Id = Guid.NewGuid(), Name = "Basic" },
                    new AccessLevelEntity { Id = Guid.NewGuid(), Name = "Enhanced" },
                    new AccessLevelEntity { Id = Guid.NewGuid(), Name = "Admin" }
                );
            }

            

            if (context.Semesters.Count().Equals(0))
            {
                context.Semesters.AddOrUpdate(x => x.Id,
                    new SemesterEntity { Id = Guid.NewGuid(), Name = "Fall" },
                    new SemesterEntity { Id = Guid.NewGuid(), Name = "Spring" },
                    new SemesterEntity { Id = Guid.NewGuid(), Name = "Summer" }
                );
            }
            

            // add school type

            if (context.SchoolTypes.Count().Equals(0))
            {
                context.SchoolTypes.AddOrUpdate(x => x.Id,
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "College, Four Year" },
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "College, Two Year" },
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "High School" },
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "Middle School" },
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "Elementary School" },
                    new SchoolTypeEntity { Id = Guid.NewGuid(), Name = "Professional/Tech Course" }
                );
            }
            

            // add test school

            // add test instructor

            // add test student

        }
    }
}

