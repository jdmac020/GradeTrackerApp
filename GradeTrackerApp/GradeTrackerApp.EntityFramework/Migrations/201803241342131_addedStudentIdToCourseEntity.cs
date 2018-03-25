namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStudentIdToCourseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseEntities", "StudentId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseEntities", "StudentId");
        }
    }
}
