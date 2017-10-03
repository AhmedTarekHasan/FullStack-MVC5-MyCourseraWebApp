using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class AdminMessageNotificationConfigurations : EntityTypeConfiguration<AdminMessageNotification>
    {
        public AdminMessageNotificationConfigurations()
        {
            this.ToTable("AdminMessageNotifications");
            this.Property(n => n.Message).IsRequired();
        }
    }
}