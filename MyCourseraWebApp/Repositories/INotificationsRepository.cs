using System.Collections.Generic;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications.NotificationsAntennas;

namespace MyCourseraWebApp.Repositories
{
    public interface INotificationsRepository
    {
        AdminMessageNotification AddAdminMessageReceivedNotification(string message, string userId);
        void AssignAdminMessageReceivedNotification(AdminMessageNotification notification, out Dictionary<string, List<int>> affectedUsersIds);
        void AssignAdminMessageReceivedNotification(AdminMessageNotification notification, string userId);
        CourseCancelledNotification AddCourseCancelledNotification(Course course, string userId);
        void AssignCourseCancelledNotification(CourseCancelledNotification notification, out Dictionary<string, List<int>> affectedUsersIds);
        CourseCreatedNotification AddCourseCreatedNotification(Course course, string userId);
        void AssignCourseCreatedNotification(CourseCreatedNotification notification, out Dictionary<string, List<int>> affectedUsersIds);
        CourseUpdatedNotification AddCourseUpdatedNotification(Course course, string userId);
        void AddCourseUpdatedNotificationDetails(CourseUpdatedNotification notification, List<CourseUpdateToken> tokens);
        void AssignCourseUpdatedNotification(CourseUpdatedNotification notification, out Dictionary<string, List<int>> affectedUsersIds);
        UserNotificationsDto GetUserNotifications(string userId);
        UserNotificationsDto GetUserNotificationsUpdates(string userId, List<int> notificationIds);
        bool MarkUserNotificationAsSeen(string userId, int notificationId);
        bool MarkUserNotificationAsUnSeen(string userId, int notificationId);
        bool MarkUserNotificationAsDismissed(string userId, int notificationId);
    }
}