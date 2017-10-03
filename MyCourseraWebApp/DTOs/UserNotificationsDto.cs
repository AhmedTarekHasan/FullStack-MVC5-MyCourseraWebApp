using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.DTOs
{
    public class UserNotificationsDto
    {
        public UserNotificationsDto()
        {
            AdminMessageUserNotifications = new List<AdminMessageUserNotificationDto>();
            CourseCreatedUserNotifications = new List<CourseCreatedUserNotificationDto>();
            CourseCancelledUserNotifications = new List<CourseCancelledUserNotificationDto>();
            CourseUpdatedUserNotifications = new List<CourseUpdatedUserNotificationDto>();
        }

        public List<AdminMessageUserNotificationDto> AdminMessageUserNotifications { get; set; }
        public List<CourseCreatedUserNotificationDto> CourseCreatedUserNotifications { get; set; }
        public List<CourseCancelledUserNotificationDto> CourseCancelledUserNotifications { get; set; }
        public List<CourseUpdatedUserNotificationDto> CourseUpdatedUserNotifications { get; set; }
    }
}