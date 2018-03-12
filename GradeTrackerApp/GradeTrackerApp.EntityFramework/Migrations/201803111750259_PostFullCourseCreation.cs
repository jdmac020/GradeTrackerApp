namespace GradeTrackerApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostFullCourseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessLevelEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Department = c.String(),
                        Number = c.String(),
                        SchoolId = c.Guid(),
                        InstructorId = c.Guid(),
                        Year = c.Int(nullable: false),
                        SemesterId = c.Guid(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalPointsPossible = c.Double(nullable: false),
                        CurrentPointsPossible = c.Double(nullable: false),
                        PointsEarned = c.Double(nullable: false),
                        EvaluationCount = c.Int(nullable: false),
                        CurrentPointsGrade = c.Double(nullable: false),
                        FinalPointsGrade = c.Double(nullable: false),
                        CurrentLetterGrade = c.String(),
                        FinalLetterGrade = c.String(),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                        StudentEntity_Id = c.String(maxLength: 128),
                        StudentEntity_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentEntity_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentEntity_Id1)
                .Index(t => t.StudentEntity_Id)
                .Index(t => t.StudentEntity_Id1);
            
            CreateTable(
                "dbo.EvaluationEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        WeightId = c.Guid(nullable: false),
                        NumberOfScores = c.Int(nullable: false),
                        PointsPossible = c.Double(nullable: false),
                        PointsEarned = c.Double(nullable: false),
                        CurrentPointsGrade = c.Double(nullable: false),
                        FinalPointsGrade = c.Double(nullable: false),
                        DropLowest = c.Boolean(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GradeThresholdEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        Name = c.String(),
                        MinimumPoints = c.Double(nullable: false),
                        MaximumPoints = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstructorEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SchoolEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        SchoolTypeId = c.Guid(nullable: false),
                        StudentEntity_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentEntity_Id)
                .Index(t => t.StudentEntity_Id);
            
            CreateTable(
                "dbo.SchoolTypeEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScoreEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EvaluationId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PointsPossible = c.Double(nullable: false),
                        PointsEarned = c.Double(nullable: false),
                        PointsGrade = c.Double(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SemesterEntities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AccessLevelId = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolEntities", "StudentEntity_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseEntities", "StudentEntity_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseEntities", "StudentEntity_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SchoolEntities", new[] { "StudentEntity_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CourseEntities", new[] { "StudentEntity_Id1" });
            DropIndex("dbo.CourseEntities", new[] { "StudentEntity_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SemesterEntities");
            DropTable("dbo.ScoreEntities");
            DropTable("dbo.SchoolTypeEntities");
            DropTable("dbo.SchoolEntities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InstructorEntities");
            DropTable("dbo.GradeThresholdEntities");
            DropTable("dbo.EvaluationEntities");
            DropTable("dbo.CourseEntities");
            DropTable("dbo.AccessLevelEntities");
        }
    }
}
