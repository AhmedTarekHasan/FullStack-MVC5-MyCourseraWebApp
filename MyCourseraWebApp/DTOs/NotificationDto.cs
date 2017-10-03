using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public UserDto ByWhom { get; set; }
        public DateTime When { get; set; }
    }
}