using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseCreatedNotificationConfigurations : EntityTypeConfiguration<CourseCreatedNotification>
    {
        public CourseCreatedNotificationConfigurations()
        {
            this.ToTable("CourseCreatedNotifications");
            this.HasRequired(n => n.Course).WithMany(c => c.CourseCreatedNotifications).HasForeignKey(n => n.CourseId);
        }
    }
}