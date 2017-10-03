using System;
using MyCourseraWebApp.Notifications.NotificationsAntennas;

namespace MyCourseraWebApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUsersRepository ApplicationUsersRepository { get; }
        ICoursesRepository CoursesRepository { get; }
        INotificationsRepository NotificationsRepository { get; }
        ISystemAntenna SystemAntenna { get; }
        ICoursesAntenna CoursesAntenna { get; }
        void Complete();
    }
}