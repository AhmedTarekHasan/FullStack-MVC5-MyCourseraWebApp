using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    /*public class NotificationTypeConfigurations : EntityTypeConfiguration<NotificationType>
    {
        public NotificationTypeConfigurations()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
            this.Property(t => t.Description).IsOptional().HasMaxLength(100);
            this.HasMany(t => t.Notifications).WithRequired(n => n.Type).HasForeignKey(n => n.TypeId);
        }
    }*/
}