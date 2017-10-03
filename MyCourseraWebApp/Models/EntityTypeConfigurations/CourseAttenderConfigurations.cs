
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseAttenderConfigurations : EntityTypeConfiguration<CourseAttender>
    {
        public CourseAttenderConfigurations()
        {
            this.HasKey(ci => new { ci.CourseId, ci.AttenderId });
            this.Property(ci => ci.CourseId).HasColumnOrder(0);
            this.Property(ci => ci.AttenderId).HasColumnOrder(1);
        }
    }
}