using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class ApplicationUserConfigurations : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfigurations()
        {
            this.Property(u => u.FullName).IsRequired();
            this.Property(u => u.UserTypeId).IsRequired();
            this.HasRequired(u => u.UserType).WithMany(t => t.Users).HasForeignKey(u => u.UserTypeId);
        }
    }
}