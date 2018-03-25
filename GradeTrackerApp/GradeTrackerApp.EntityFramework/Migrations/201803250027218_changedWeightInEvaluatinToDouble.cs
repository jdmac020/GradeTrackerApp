namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedWeightInEvaluatinToDouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvaluationEntities", "Weight", c => c.Double(nullable: false));
            DropColumn("dbo.EvaluationEntities", "WeightId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvaluationEntities", "WeightId", c => c.Guid(nullable: false));
            DropColumn("dbo.EvaluationEntities", "Weight");
        }
    }
}
