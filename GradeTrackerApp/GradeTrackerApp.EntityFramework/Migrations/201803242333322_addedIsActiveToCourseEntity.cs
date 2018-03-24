namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsActiveToCourseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseEntities", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseEntities", "IsActive");
        }
    }
}
