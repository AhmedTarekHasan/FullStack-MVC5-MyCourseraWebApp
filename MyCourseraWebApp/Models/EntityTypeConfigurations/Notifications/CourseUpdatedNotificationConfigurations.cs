using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseUpdatedNotificationConfigurations : EntityTypeConfiguration<CourseUpdatedNotification>
    {
        public CourseUpdatedNotificationConfigurations()
        {
            this.ToTable("CourseUpdatedNotifications");
            this.HasRequired(n => n.Course).WithMany(c => c.CourseUpdatedNotifications).HasForeignKey(n => n.CourseId);
            this.HasMany(n => n.NotificationDetails).WithRequired(d => d.Notification);
        }
    }
}