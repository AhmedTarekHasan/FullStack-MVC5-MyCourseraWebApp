using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.DTOs
{
    public class CourseCreatedNotificationDto
    {
        public NotificationDto Notification { get; set; }
        public CourseDto Course { get; set; }
    }
}