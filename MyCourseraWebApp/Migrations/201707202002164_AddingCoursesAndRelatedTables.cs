namespace MyCourseraWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCoursesAndRelatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAttenders",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AttenderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AttenderId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttenderId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.AttenderId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseInstructors",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        InstructorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.InstructorId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructors", "InstructorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseInstructors", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseAttenders", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseAttenders", "AttenderId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseInstructors", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructors", new[] { "CourseId" });
            DropIndex("dbo.CourseAttenders", new[] { "AttenderId" });
            DropIndex("dbo.CourseAttenders", new[] { "CourseId" });
            DropTable("dbo.CourseInstructors");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseAttenders");
        }
    }
}
