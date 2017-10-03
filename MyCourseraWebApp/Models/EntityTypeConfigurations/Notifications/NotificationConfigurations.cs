
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class NotificationConfigurations : EntityTypeConfiguration<Notification>
    {
        public NotificationConfigurations()
        {
            this.HasKey(n => n.Id);
            this.Property(n => n.When).IsRequired();
            //this.Property(n => n.Type).IsRequired();
            this.HasRequired(n => n.ByWhom).WithMany(u => u.Notifications).HasForeignKey(n => n.ByWhomId);
        }
    }
}