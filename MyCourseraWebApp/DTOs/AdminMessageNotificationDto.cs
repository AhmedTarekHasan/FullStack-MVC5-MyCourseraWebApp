using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.DTOs
{
    public class AdminMessageNotificationDto
    {
        public NotificationDto Notification { get; set; }
        public string Message { get; set; }
    }
}