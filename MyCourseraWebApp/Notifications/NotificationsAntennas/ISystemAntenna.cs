using System.Collections.Generic;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Notifications.NotificationsAntennas
{
    public delegate void AdminMessageDelegate(string message);
    public delegate void UserRegisterationSuccessDelegate(string userId);

    public interface ISystemAntenna
    {
        event AdminMessageDelegate AdminMessageReceived;
        event UserRegisterationSuccessDelegate UserRegisterationSuccess;

        void OnAdminMessageReceived(string message);
        void OnUserRegisterationSuccess(string userId);
    }
}