using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MyCourseraWebApp.Hubs;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications;
using MyCourseraWebApp.Notifications.NotificationsAntennas;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp.Helpers
{
    public static class ContextHelpers
    {
        #region Fields
        private static ISystemAntenna globalSystemAntenna;
        private static ICoursesAntenna globalCoursesAntenna;
        private static INotificationsManager globalNotificationsManager;
        private static ConnectionMapping<string> connectedConnections = new ConnectionMapping<string>();
        #endregion

        #region Properties
        public static ApplicationUser CurrentLoggedInUser
        {
            get
            {
                ApplicationUser user = null;

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string userId = HttpContext.Current.User.Identity.GetUserId();

                    if (!string.IsNullOrEmpty(userId))
                    {
                        IUnitOfWork unity = new UnitOfWork(new ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
                        user = unity.ApplicationUsersRepository.GetUserById(userId);
                    }
                }

                return user;
            }
        }
        public static ApplicationUser SystemAdmin
        {
            get
            {
                IUnitOfWork unity = new UnitOfWork(new ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
                return unity.ApplicationUsersRepository.GetSuperAdmin();
            }
        }
        public static int DefaultPageSize
        {
            get
            {
                int result = int.MaxValue;

                if (ConfigurationManager.AppSettings["PageSize"] != null)
                {
                    if (int.TryParse(ConfigurationManager.AppSettings["PageSize"], out result) && result <= 0)
                    {
                        result = int.MaxValue;
                    }
                }

                return result;
            }
        }
        public static int DefaultPagerWidth
        {
            get
            {
                int result = 3;

                if (ConfigurationManager.AppSettings["PagerWidth"] != null)
                {
                    if (int.TryParse(ConfigurationManager.AppSettings["PagerWidth"], out result) && result <= 0)
                    {
                        result = 3;
                    }
                }

                return result;
            }
        }
        public static ISystemAntenna GlobalSystemAntenna
        {
            get
            {
                if (globalSystemAntenna == null)
                {
                    globalSystemAntenna = new SystemAntenna();
                }

                return globalSystemAntenna;
            }
        }
        public static ICoursesAntenna GlobalCoursesAntenna
        {
            get
            {
                if (globalCoursesAntenna == null)
                {
                    globalCoursesAntenna = new CoursesAntenna();
                }

                return globalCoursesAntenna;
            }
        }
        public static INotificationsManager GlobalNotificationsManager
        {
            get
            {
                if (globalNotificationsManager == null)
                {
                    globalNotificationsManager = new NotificationsManager(new UnitOfWork(new ApplicationDbContext(), GlobalSystemAntenna, GlobalCoursesAntenna));
                }

                return globalNotificationsManager;
            }
        }
        public static ConnectionMapping<string> ConnectedConnections
        {
            get
            {
                return connectedConnections;
            }
        }
        #endregion
    }
}