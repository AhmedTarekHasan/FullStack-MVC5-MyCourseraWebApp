namespace MyCourseraWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNotificationsEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        When = c.DateTime(nullable: false),
                        ByWhomId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ByWhomId, cascadeDelete: true)
                .Index(t => t.ByWhomId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsDismissed = c.Boolean(nullable: false),
                        IsSeen = c.Boolean(nullable: false),
                        IsPinned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NotificationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        FieldName = c.String(maxLength: 50),
                        OldValue = c.String(maxLength: 1500),
                        NewValue = c.String(maxLength: 1500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseUpdatedNotifications", t => t.NotificationId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.AdminMessageNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CourseCancelledNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.CourseCreatedNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.CourseUpdatedNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notifications", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseUpdatedNotifications", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseUpdatedNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.CourseCreatedNotifications", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseCreatedNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.CourseCancelledNotifications", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseCancelledNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.AdminMessageNotifications", "Id", "dbo.Notifications");
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.NotificationDetails", "NotificationId", "dbo.CourseUpdatedNotifications");
            DropForeignKey("dbo.Notifications", "ByWhomId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseUpdatedNotifications", new[] { "CourseId" });
            DropIndex("dbo.CourseUpdatedNotifications", new[] { "Id" });
            DropIndex("dbo.CourseCreatedNotifications", new[] { "CourseId" });
            DropIndex("dbo.CourseCreatedNotifications", new[] { "Id" });
            DropIndex("dbo.CourseCancelledNotifications", new[] { "CourseId" });
            DropIndex("dbo.CourseCancelledNotifications", new[] { "Id" });
            DropIndex("dbo.AdminMessageNotifications", new[] { "Id" });
            DropIndex("dbo.NotificationDetails", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.Notifications", new[] { "ByWhomId" });
            DropTable("dbo.CourseUpdatedNotifications");
            DropTable("dbo.CourseCreatedNotifications");
            DropTable("dbo.CourseCancelledNotifications");
            DropTable("dbo.AdminMessageNotifications");
            DropTable("dbo.NotificationDetails");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
