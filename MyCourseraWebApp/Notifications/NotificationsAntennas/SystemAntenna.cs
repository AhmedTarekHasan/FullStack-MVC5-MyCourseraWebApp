using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Notifications.NotificationsAntennas
{
    public class SystemAntenna : ISystemAntenna
    {
        public event AdminMessageDelegate AdminMessageReceived;
        public event UserRegisterationSuccessDelegate UserRegisterationSuccess;

        public void OnAdminMessageReceived(string message)
        {
            if (AdminMessageReceived != null)
            {
                AdminMessageReceived(message);
            }
        }
        public void OnUserRegisterationSuccess(string userId)
        {
            if (UserRegisterationSuccess != null)
            {
                UserRegisterationSuccess(userId);
            }
        }
    }
}