using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models.EntityTypeConfigurations
{
    public class CourseInstructorConfigurations : EntityTypeConfiguration<CourseInstructor>
    {
        public CourseInstructorConfigurations()
        {
            this.HasKey(ci => new { ci.CourseId, ci.InstructorId });
            this.Property(ci => ci.CourseId).HasColumnOrder(0);
            this.Property(ci => ci.InstructorId).HasColumnOrder(1);
        }
    }
}