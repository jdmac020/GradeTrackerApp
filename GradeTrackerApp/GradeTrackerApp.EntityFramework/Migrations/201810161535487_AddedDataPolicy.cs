namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataPolicy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataPolicyAcceptedDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DataPolicyAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DataPolicyAccepted");
            DropColumn("dbo.AspNetUsers", "DataPolicyAcceptedDate");
        }
    }
}
