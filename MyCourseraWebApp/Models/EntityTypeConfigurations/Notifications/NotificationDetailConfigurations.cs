using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class NotificationDetailConfigurations : EntityTypeConfiguration<NotificationDetail>
    {
        public NotificationDetailConfigurations()
        {
            this.HasKey(d => d.Id);
            this.Property(d => d.FieldName).HasMaxLength(50);
            this.Property(d => d.OldValue).HasMaxLength(1500);
            this.Property(d => d.NewValue).HasMaxLength(1500);
            this.HasRequired(d => d.Notification).WithMany(n => n.NotificationDetails).HasForeignKey(d => d.NotificationId);
        }
    }
}