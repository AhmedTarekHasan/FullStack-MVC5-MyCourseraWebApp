using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CommonUtilities;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications.NotificationsAntennas;

namespace MyCourseraWebApp.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        #region Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructors
        public NotificationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region INotificationsRepository Implementations
        public AdminMessageNotification AddAdminMessageReceivedNotification(string message, string userId)
        {
            return _context.AdminMessageNotifications.Add(new AdminMessageNotification(message, null, userId));
        }
        public void AssignAdminMessageReceivedNotification(AdminMessageNotification notification, out Dictionary<string, List<int>> affectedUsersIds)
        {
            affectedUsersIds = new Dictionary<string, List<int>>();

            if (notification != null)
            {
                var usersNotifications =
                (
                    from user in _context.Users
                    select user.Id
                ).ToList<string>().Select(id => new UserNotification(notification.Id, id));

                if (usersNotifications != null && usersNotifications.Count() > 0)
                {
                    _context.UserNotifications.AddRange(usersNotifications);

                    foreach (UserNotification un in usersNotifications)
                    {
                        if (!affectedUsersIds.ContainsKey(un.UserId))
                        {
                            affectedUsersIds.Add(un.UserId, new List<int>());
                        }

                        affectedUsersIds[un.UserId].Add(un.NotificationId);
                    }
                }
            }
        }
        public void AssignAdminMessageReceivedNotification(AdminMessageNotification notification, string userId)
        {
            _context.UserNotifications.Add(new UserNotification(notification.Id, userId));
        }
        public CourseCancelledNotification AddCourseCancelledNotification(Course course, string userId)
        {
            return _context.CourseCancelledNotifications.Add(new CourseCancelledNotification(null, course.Id, null, userId));
        }
        public void AssignCourseCancelledNotification(CourseCancelledNotification notification, out Dictionary<string, List<int>> affectedUsersIds)
        {
            affectedUsersIds = new Dictionary<string, List<int>>();

            if (notification != null)
            {
                var attenderNotifications =
                (
                    from attender in _context.CoursesAttenders
                    where attender.CourseId == notification.CourseId
                    select attender.AttenderId
                ).ToList<string>().Distinct().Select(id => new UserNotification(notification.Id, id));

                if (attenderNotifications != null && attenderNotifications.Count() > 0)
                {
                    _context.UserNotifications.AddRange(attenderNotifications);

                    foreach (UserNotification un in attenderNotifications)
                    {
                        if (!affectedUsersIds.ContainsKey(un.UserId))
                        {
                            affectedUsersIds.Add(un.UserId, new List<int>());
                        }

                        affectedUsersIds[un.UserId].Add(un.NotificationId);
                    }
                }
            }
        }
        public CourseCreatedNotification AddCourseCreatedNotification(Course course, string userId)
        {
            return _context.CourseCreatedNotifications.Add(new CourseCreatedNotification(null, course.Id, null, userId));
        }
        public void AssignCourseCreatedNotification(CourseCreatedNotification notification, out Dictionary<string, List<int>> affectedUsersIds)
        {
            affectedUsersIds = new Dictionary<string, List<int>>();

            if (notification != null)
            {
                var attenderNotifications =
                (
                    from attender in _context.CoursesAttenders.Distinct()
                    select attender.AttenderId
                ).ToList<string>().Distinct().Select(id => new UserNotification(notification.Id, id));

                if (attenderNotifications != null && attenderNotifications.Count() > 0)
                {
                    _context.UserNotifications.AddRange(attenderNotifications);

                    foreach (UserNotification un in attenderNotifications)
                    {
                        if (!affectedUsersIds.ContainsKey(un.UserId))
                        {
                            affectedUsersIds.Add(un.UserId, new List<int>());
                        }

                        affectedUsersIds[un.UserId].Add(un.NotificationId);
                    }
                }
            }
        }
        public CourseUpdatedNotification AddCourseUpdatedNotification(Course course, string userId)
        {
            CourseUpdatedNotification notification = new CourseUpdatedNotification(null, course.Id, null, userId);
            return _context.CourseUpdatedNotifications.Add(notification);
        }
        public void AddCourseUpdatedNotificationDetails(CourseUpdatedNotification notification, List<CourseUpdateToken> tokens)
        {
            if (notification != null)
            {
                var details =
                (
                    from token in tokens
                    select new NotificationDetail(notification.Id, token.PropertyName, token.OldValue, token.NewValue)
                ).ToList();

                if (details != null && details.Count > 0)
                {
                    foreach (var detail in details)
                    {
                        notification.NotificationDetails.Add(detail);
                    }
                }
            }
        }
        public void AssignCourseUpdatedNotification(CourseUpdatedNotification notification, out Dictionary<string, List<int>> affectedUsersIds)
        {
            affectedUsersIds = new Dictionary<string, List<int>>();

            if (notification != null)
            {
                var attenderNotifications =
                (
                    from attender in _context.CoursesAttenders
                    where attender.CourseId == notification.CourseId
                    select attender.AttenderId
                ).ToList<string>().Select(id => new UserNotification(notification.Id, id));

                if (attenderNotifications != null && attenderNotifications.Count() > 0)
                {
                    _context.UserNotifications.AddRange(attenderNotifications);

                    foreach (UserNotification un in attenderNotifications)
                    {
                        if (!affectedUsersIds.ContainsKey(un.UserId))
                        {
                            affectedUsersIds.Add(un.UserId, new List<int>());
                        }

                        affectedUsersIds[un.UserId].Add(un.NotificationId);
                    }
                }
            }
        }
        public UserNotificationsDto GetUserNotifications(string userId)
        {
            return GetFullUserNotifications(userId);
        }
        public UserNotificationsDto GetUserNotificationsUpdates(string userId, List<int> notificationIds)
        {
            return GetFullUserNotifications(userId, notificationIds);
        }
        public bool MarkUserNotificationAsSeen(string userId, int notificationId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId) && notificationId > 0)
            {
                UserNotification userNotification = _context.UserNotifications.DefaultIfEmpty(null).SingleOrDefault(un => un.UserId == userId && un.NotificationId == notificationId);

                if (userNotification != null)
                {
                    userNotification.MarkAsSeen();
                    result = true;
                }
            }

            return result;
        }
        public bool MarkUserNotificationAsUnSeen(string userId, int notificationId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId) && notificationId > 0)
            {
                UserNotification userNotification = _context.UserNotifications.DefaultIfEmpty(null).SingleOrDefault(un => un.UserId == userId && un.NotificationId == notificationId);

                if (userNotification != null)
                {
                    userNotification.MarkAsUnSeen();
                    result = true;
                }
            }

            return result;
        }
        public bool MarkUserNotificationAsDismissed(string userId, int notificationId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId) && notificationId > 0)
            {
                UserNotification userNotification = _context.UserNotifications.DefaultIfEmpty(null).SingleOrDefault(un => un.UserId == userId && un.NotificationId == notificationId);

                if (userNotification != null)
                {
                    userNotification.Dismiss();
                    result = true;
                }
            }

            return result;
        }
        #endregion

        #region Helper Methods
        private UserNotificationsDto GetFullUserNotifications(string userId, List<int> notificationIds = null)
        {
            UserNotificationsDto result = new UserNotificationsDto();

            if (notificationIds == null)
            {
                notificationIds = new List<int>();
            }

            result.AdminMessageUserNotifications =
            (
                from userNotification in _context.UserNotifications
                join adminMessageNotification in _context.AdminMessageNotifications.Include(n => n.ByWhom) on new { NotificationId = userNotification.NotificationId, UserId = userNotification.UserId } equals new { NotificationId = adminMessageNotification.Id, UserId = userId }
                where userNotification.IsDismissed == false && (notificationIds.Count == 0 || notificationIds.Contains(userNotification.NotificationId))
                select new AdminMessageUserNotificationDto()
                {
                    Notification = new AdminMessageNotificationDto()
                    {
                        Notification = new NotificationDto()
                        {
                            Id = adminMessageNotification.Id,
                            When = adminMessageNotification.When,
                            ByWhom = new UserDto()
                            {
                                Id = adminMessageNotification.ByWhom.Id,
                                UserName = adminMessageNotification.ByWhom.UserName,
                                FullName = adminMessageNotification.ByWhom.FullName,
                                Email = adminMessageNotification.ByWhom.Email,
                                Type = adminMessageNotification.ByWhom.UserType.Name.ToString()
                            }
                        },
                        Message = adminMessageNotification.Message
                    },
                    IsSeen = userNotification.IsSeen,
                    IsDismissed = userNotification.IsDismissed
                }
            ).ToList();

            result.CourseCreatedUserNotifications =
            (
                from userNotification in _context.UserNotifications
                join courseCreatedNotification in _context.CourseCreatedNotifications.Include(n => n.ByWhom).Include(n => n.Course) on new { NotificationId = userNotification.NotificationId, UserId = userNotification.UserId } equals new { NotificationId = courseCreatedNotification.Id, UserId = userId }
                where userNotification.IsDismissed == false && (notificationIds.Count == 0 || notificationIds.Contains(userNotification.NotificationId))
                select new CourseCreatedUserNotificationDto()
                {
                    Notification = new CourseCreatedNotificationDto()
                    {
                        Notification = new NotificationDto()
                        {
                            Id = courseCreatedNotification.Id,
                            When = courseCreatedNotification.When,
                            ByWhom = new UserDto()
                            {
                                Id = courseCreatedNotification.ByWhom.Id,
                                UserName = courseCreatedNotification.ByWhom.UserName,
                                FullName = courseCreatedNotification.ByWhom.FullName,
                                Email = courseCreatedNotification.ByWhom.Email,
                                Type = courseCreatedNotification.ByWhom.UserType.Name.ToString()
                            }
                        },
                        Course = new CourseDto()
                        {
                            CourseId = courseCreatedNotification.CourseId,
                            Title = courseCreatedNotification.Course.Title,
                            Description = courseCreatedNotification.Course.Description,
                            From = courseCreatedNotification.Course.From,
                            To = courseCreatedNotification.Course.To,
                            IsCancelled = courseCreatedNotification.Course.IsCancelled
                        }
                    },
                    IsSeen = userNotification.IsSeen,
                    IsDismissed = userNotification.IsDismissed
                }
            ).ToList();

            result.CourseCancelledUserNotifications =
            (
                from userNotification in _context.UserNotifications
                join courseCancelledNotification in _context.CourseCancelledNotifications.Include(n => n.ByWhom).Include(n => n.Course) on new { NotificationId = userNotification.NotificationId, UserId = userNotification.UserId } equals new { NotificationId = courseCancelledNotification.Id, UserId = userId }
                where userNotification.IsDismissed == false && (notificationIds.Count == 0 || notificationIds.Contains(userNotification.NotificationId))
                select new CourseCancelledUserNotificationDto()
                {
                    Notification = new CourseCancelledNotificationDto()
                    {
                        Notification = new NotificationDto()
                        {
                            Id = courseCancelledNotification.Id,
                            When = courseCancelledNotification.When,
                            ByWhom = new UserDto()
                            {
                                Id = courseCancelledNotification.ByWhom.Id,
                                UserName = courseCancelledNotification.ByWhom.UserName,
                                FullName = courseCancelledNotification.ByWhom.FullName,
                                Email = courseCancelledNotification.ByWhom.Email,
                                Type = courseCancelledNotification.ByWhom.UserType.Name.ToString()
                            }
                        },
                        Course = new CourseDto()
                        {
                            CourseId = courseCancelledNotification.CourseId,
                            Title = courseCancelledNotification.Course.Title,
                            Description = courseCancelledNotification.Course.Description,
                            From = courseCancelledNotification.Course.From,
                            To = courseCancelledNotification.Course.To,
                            IsCancelled = courseCancelledNotification.Course.IsCancelled
                        }
                    },
                    IsSeen = userNotification.IsSeen,
                    IsDismissed = userNotification.IsDismissed
                }
            ).ToList();

            result.CourseUpdatedUserNotifications = (
                from userNotification in _context.UserNotifications
                join courseUpdatedNotification in _context.CourseUpdatedNotifications.Include(n => n.ByWhom).Include(n => n.Course).Include(n => n.NotificationDetails) on new { NotificationId = userNotification.NotificationId, UserId = userNotification.UserId } equals new { NotificationId = courseUpdatedNotification.Id, UserId = userId }
                where userNotification.IsDismissed == false && (notificationIds.Count == 0 || notificationIds.Contains(userNotification.NotificationId))
                select new CourseUpdatedUserNotificationDto()
                {
                    Notification = new CourseUpdatedNotificationDto()
                    {
                        Notification = new NotificationDto()
                        {
                            Id = courseUpdatedNotification.Id,
                            When = courseUpdatedNotification.When,
                            ByWhom = new UserDto()
                            {
                                Id = courseUpdatedNotification.ByWhom.Id,
                                UserName = courseUpdatedNotification.ByWhom.UserName,
                                FullName = courseUpdatedNotification.ByWhom.FullName,
                                Email = courseUpdatedNotification.ByWhom.Email,
                                Type = courseUpdatedNotification.ByWhom.UserType.Name.ToString()
                            }
                        },
                        Course = new CourseDto()
                        {
                            CourseId = courseUpdatedNotification.CourseId,
                            Title = courseUpdatedNotification.Course.Title,
                            Description = courseUpdatedNotification.Course.Description,
                            From = courseUpdatedNotification.Course.From,
                            To = courseUpdatedNotification.Course.To,
                            IsCancelled = courseUpdatedNotification.Course.IsCancelled
                        },
                        Details =
                        (
                            from detail in courseUpdatedNotification.NotificationDetails
                            select new NotificationDetailDto()
                            {
                                Id = detail.Id,
                                FieldName = detail.FieldName,
                                OldValue = detail.OldValue,
                                NewValue = detail.NewValue
                            }
                        ).ToList()
                    },
                    IsSeen = userNotification.IsSeen,
                    IsDismissed = userNotification.IsDismissed
                }
            ).ToList();

            return result;
        }
        #endregion
    }
}