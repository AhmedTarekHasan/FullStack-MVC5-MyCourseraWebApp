using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class UserNotificationConfigurations : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfigurations()
        {
            this.HasKey(un => un.Id);
            this.HasRequired(un => un.Notification).WithMany(n => n.UsersNotifications).HasForeignKey(un => un.NotificationId);
            this.HasRequired(un => un.User).WithMany(n => n.UsersNotifications).HasForeignKey(un => un.UserId).WillCascadeOnDelete(false);
            this.Property(un => un.IsDismissed).IsRequired();
            this.Property(un => un.IsPinned).IsRequired();
            this.Property(un => un.IsSeen).IsRequired();
        }
    }
}