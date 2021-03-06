﻿using GradeTrackerApp.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GradeTrackerApp.EntityFramework.Contexts
{
    public class GradeTrackerDbContext : IdentityDbContext<StudentEntity>
    {
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<EvaluationEntity> Evaluations { get; set; }
        public DbSet<GradeThresholdEntity> GradeThresholds { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<SchoolEntity> Schools { get; set; }
        public DbSet<SchoolTypeEntity> SchoolTypes { get; set; }
        public DbSet<ScoreEntity> Scores { get; set; }
        public DbSet<SemesterEntity> Semesters { get; set; }
        public DbSet<AccessLevelEntity> AccessLevels { get; set; }

        public GradeTrackerDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GradeTrackerDbContext, Migrations.Configuration>());
        }

        public GradeTrackerDbContext() : base("name=GradeTrackerDbContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GradeTrackerDbContext, Migrations.Configuration>());
        }

        public static GradeTrackerDbContext Create()
        {
            return new GradeTrackerDbContext();
        }
    }
}