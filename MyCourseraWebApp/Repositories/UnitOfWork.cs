using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications.NotificationsAntennas;

namespace MyCourseraWebApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext context, ISystemAntenna systemAntenna, ICoursesAntenna coursesAntenna)
        {
            _context = context;
            ApplicationUsersRepository = new ApplicationUsersRepository(_context);
            CoursesRepository = new CoursesRepository(_context);
            NotificationsRepository = new NotificationsRepository(_context);
            SystemAntenna = systemAntenna;
            CoursesAntenna = coursesAntenna;
        }
        #endregion

        #region IUnitOfWork Implementations
        public IApplicationUsersRepository ApplicationUsersRepository { get; private set; }
        public ICoursesRepository CoursesRepository { get; private set; }
        public INotificationsRepository NotificationsRepository { get; private set; }
        public ISystemAntenna SystemAntenna { get; private set; }
        public ICoursesAntenna CoursesAntenna { get; private set; }
        public void Complete()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}