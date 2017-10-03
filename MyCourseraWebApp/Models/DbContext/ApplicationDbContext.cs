using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MyCourseraWebApp.Models.EntityTypeConfigurations;

namespace MyCourseraWebApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructors
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        #endregion

        #region Properties
        public DbSet<ApplicationUserType> ApplicationUserTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstructor> CoursesInstructors { get; set; }
        public DbSet<CourseAttender> CoursesAttenders { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AdminMessageNotification> AdminMessageNotifications { get; set; }
        public DbSet<CourseCreatedNotification> CourseCreatedNotifications { get; set; }
        public DbSet<CourseUpdatedNotification> CourseUpdatedNotifications { get; set; }
        public DbSet<CourseCancelledNotification> CourseCancelledNotifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        #endregion

        #region Overrides
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfigurations());
            modelBuilder.Configurations.Add(new ApplicationUserTypeConfigurations());
            modelBuilder.Configurations.Add(new CourseConfigurations());
            modelBuilder.Configurations.Add(new CourseInstructorConfigurations());
            modelBuilder.Configurations.Add(new CourseAttenderConfigurations());
            modelBuilder.Configurations.Add(new NotificationConfigurations());
            modelBuilder.Configurations.Add(new AdminMessageNotificationConfigurations());
            modelBuilder.Configurations.Add(new CourseCreatedNotificationConfigurations());
            modelBuilder.Configurations.Add(new CourseUpdatedNotificationConfigurations());
            modelBuilder.Configurations.Add(new CourseCancelledNotificationConfigurations());
            modelBuilder.Configurations.Add(new NotificationDetailConfigurations());
            modelBuilder.Configurations.Add(new UserNotificationConfigurations());

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Methods
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        #endregion
    }
}