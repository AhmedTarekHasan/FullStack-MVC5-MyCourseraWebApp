using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsCancelled { get; set; }
    }
}