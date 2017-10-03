using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Hubs;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications.NotificationsAntennas;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp.Notifications
{
    public class NotificationsManager : INotificationsManager
    {
        #region Fields
        private IUnitOfWork unitOfWork;
        #endregion

        #region Constructors
        public NotificationsManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Properties
        
        #endregion

        #region Public Methods
        public void StartListening()
        {
            unitOfWork.SystemAntenna.AdminMessageReceived += SystemAntenna_AdminMessageReceived;
            unitOfWork.SystemAntenna.UserRegisterationSuccess += SystemAntenna_UserRegisterationSuccess;
            unitOfWork.CoursesAntenna.CourseCreated += CoursesAntenna_CourseCreated;
            unitOfWork.CoursesAntenna.CourseUpdated += CoursesAntenna_CourseUpdated;
            unitOfWork.CoursesAntenna.CourseCancelled += CoursesAntenna_CourseCancelled;
        }
        #endregion

        #region Handlers
        private void SystemAntenna_UserRegisterationSuccess(string userId)
        {
            AdminMessageNotification notification = unitOfWork.NotificationsRepository.AddAdminMessageReceivedNotification("<span style='font-weight: bold;'>Welcome to MyCoursera</span>", ContextHelpers.SystemAdmin.Id);
            unitOfWork.Complete();

            unitOfWork.NotificationsRepository.AssignAdminMessageReceivedNotification(notification, userId);
            unitOfWork.Complete();

            Dictionary<string, List<int>> token = new Dictionary<string, List<int>>();
            token.Add(userId, new List<int>() { notification.Id });
            NotifyUsers(token);
        }
        private void SystemAntenna_AdminMessageReceived(string message)
        {
            AdminMessageNotification notification = unitOfWork.NotificationsRepository.AddAdminMessageReceivedNotification(message, ContextHelpers.SystemAdmin.Id);
            unitOfWork.Complete();

            Dictionary<string, List<int>> affectedUsersIds = null;
            unitOfWork.NotificationsRepository.AssignAdminMessageReceivedNotification(notification, out affectedUsersIds);
            unitOfWork.Complete();

            if (affectedUsersIds != null && affectedUsersIds.Count > 0)
            {
                NotifyUsers(affectedUsersIds);
            }
        }
        private void CoursesAntenna_CourseCancelled(Course course)
        {
            CourseCancelledNotification notification = unitOfWork.NotificationsRepository.AddCourseCancelledNotification(course, ContextHelpers.CurrentLoggedInUser.Id);
            unitOfWork.Complete();

            Dictionary<string, List<int>> affectedUsersIds = null;
            unitOfWork.NotificationsRepository.AssignCourseCancelledNotification(notification, out affectedUsersIds);
            unitOfWork.Complete();

            if (affectedUsersIds != null && affectedUsersIds.Count > 0)
            {
                NotifyUsers(affectedUsersIds);
            }
        }
        private void CoursesAntenna_CourseCreated(Course course)
        {
            if (course != null)
            {
                CourseCreatedNotification notification = unitOfWork.NotificationsRepository.AddCourseCreatedNotification(course, ContextHelpers.CurrentLoggedInUser.Id);
                unitOfWork.Complete();

                Dictionary<string, List<int>> affectedUsersIds = null;
                unitOfWork.NotificationsRepository.AssignCourseCreatedNotification(notification, out affectedUsersIds);
                unitOfWork.Complete();

                if (affectedUsersIds != null && affectedUsersIds.Count > 0)
                {
                    NotifyUsers(affectedUsersIds);
                }
            }
        }
        private void CoursesAntenna_CourseUpdated(Course course, List<CourseUpdateToken> tokens)
        {
            if (course != null)
            {
                CourseUpdatedNotification notification = unitOfWork.NotificationsRepository.AddCourseUpdatedNotification(course, ContextHelpers.CurrentLoggedInUser.Id);
                unitOfWork.Complete();

                if (tokens != null && tokens.Count > 0)
                {
                    unitOfWork.NotificationsRepository.AddCourseUpdatedNotificationDetails(notification, tokens);
                    unitOfWork.Complete();
                }

                Dictionary<string, List<int>> affectedUsersIds = null;
                unitOfWork.NotificationsRepository.AssignCourseUpdatedNotification(notification, out affectedUsersIds);
                unitOfWork.Complete();

                if (affectedUsersIds != null && affectedUsersIds.Count > 0)
                {
                    NotifyUsers(affectedUsersIds);
                }
            }
        }
        #endregion

        #region Helper Methods
        private void NotifyUsers(Dictionary<string, List<int>> usersNotifications)
        {
            if (usersNotifications != null && usersNotifications.Count > 0)
            {
                var hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
                List<string> foundConnectionIds = null;

                foreach (KeyValuePair<string, List<int>> userNotifications in usersNotifications)
                {
                    foundConnectionIds = ContextHelpers.ConnectedConnections.GetConnections(userNotifications.Key).ToList();

                    if (foundConnectionIds != null && foundConnectionIds.Count > 0)
                    {
                        foreach (string connectionId in foundConnectionIds)
                        {
                            hub.Clients.Client(connectionId).receiveNotifications(unitOfWork.NotificationsRepository.GetUserNotificationsUpdates(userNotifications.Key, userNotifications.Value));
                        }
                    }
                }
            }
        }
        #endregion
    }
}