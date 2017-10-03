using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class CourseAttender
    {
        #region Properties
        public string AttenderId { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Attender { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}