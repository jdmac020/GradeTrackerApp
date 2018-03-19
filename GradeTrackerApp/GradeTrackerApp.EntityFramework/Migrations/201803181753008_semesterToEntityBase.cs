namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semesterToEntityBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterEntities", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SemesterEntities", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SemesterEntities", "LastModified");
            DropColumn("dbo.SemesterEntities", "CreatedOn");
        }
    }
}
