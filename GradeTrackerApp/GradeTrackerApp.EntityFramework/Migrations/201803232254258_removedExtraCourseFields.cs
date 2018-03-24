namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedExtraCourseFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourseEntities", "SchoolId");
            DropColumn("dbo.CourseEntities", "InstructorId");
            DropColumn("dbo.CourseEntities", "StartTime");
            DropColumn("dbo.CourseEntities", "EndTime");
            DropColumn("dbo.CourseEntities", "StartDate");
            DropColumn("dbo.CourseEntities", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseEntities", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CourseEntities", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CourseEntities", "EndTime", c => c.DateTime());
            AddColumn("dbo.CourseEntities", "StartTime", c => c.DateTime());
            AddColumn("dbo.CourseEntities", "InstructorId", c => c.Guid());
            AddColumn("dbo.CourseEntities", "SchoolId", c => c.Guid());
        }
    }
}
