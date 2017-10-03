
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseConfigurations : EntityTypeConfiguration<Course>
    {
        public CourseConfigurations()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Title).IsRequired().HasMaxLength(200);
            this.Property(c => c.Description).IsRequired().HasMaxLength(1000);
            this.Property(c => c.From).IsRequired();
            this.Property(c => c.To).IsRequired();
            this.Property(c => c.IsCancelled).IsRequired();
            //this.HasMany(c => c.Notifications).WithRequired(n => n.Course).HasForeignKey(n => n.CourseId);
        }
    }
}