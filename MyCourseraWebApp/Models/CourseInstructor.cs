using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class CourseInstructor
    {
        #region Properties
        public string InstructorId { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Instructor { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}