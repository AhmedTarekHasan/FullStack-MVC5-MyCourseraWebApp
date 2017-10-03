using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class ApplicationUserTypeConfigurations : EntityTypeConfiguration<ApplicationUserType>
    {
        public ApplicationUserTypeConfigurations()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired();
        }
    }
}