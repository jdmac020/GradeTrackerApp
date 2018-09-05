namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class evaluationUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvaluationEntities", "PointsPerScore", c => c.Double(nullable: false));
            AddColumn("dbo.EvaluationEntities", "TotalPointsPossible", c => c.Double(nullable: false));
            AddColumn("dbo.EvaluationEntities", "CurrentPointsPossible", c => c.Double(nullable: false));
            AddColumn("dbo.EvaluationEntities", "NumberToDrop", c => c.Int(nullable: false));
            DropColumn("dbo.EvaluationEntities", "PointsPossible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvaluationEntities", "PointsPossible", c => c.Double(nullable: false));
            DropColumn("dbo.EvaluationEntities", "NumberToDrop");
            DropColumn("dbo.EvaluationEntities", "CurrentPointsPossible");
            DropColumn("dbo.EvaluationEntities", "TotalPointsPossible");
            DropColumn("dbo.EvaluationEntities", "PointsPerScore");
        }
    }
}
