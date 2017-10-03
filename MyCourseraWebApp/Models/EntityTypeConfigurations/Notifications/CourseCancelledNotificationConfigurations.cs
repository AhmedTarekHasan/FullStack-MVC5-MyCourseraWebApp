using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseCancelledNotificationConfigurations : EntityTypeConfiguration<CourseCancelledNotification>
    {
        public CourseCancelledNotificationConfigurations()
        {
            this.ToTable("CourseCancelledNotifications");
            this.HasRequired(n => n.Course).WithMany(c => c.CourseCancelledNotifications).HasForeignKey(n => n.CourseId);
        }
    }
}