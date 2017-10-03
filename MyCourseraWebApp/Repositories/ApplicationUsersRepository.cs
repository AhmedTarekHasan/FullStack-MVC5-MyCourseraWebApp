using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Repositories
{
    public class ApplicationUsersRepository : IApplicationUsersRepository
    {
        #region Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructors
        public ApplicationUsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region IApplicationUsersRepository Implementations
        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser user = null;

            if (!string.IsNullOrEmpty(id))
            {
                user = _context.Users.DefaultIfEmpty(null).SingleOrDefault(u => u.Id == id);
            }

            return user;
        }
        public ApplicationUser GetSuperAdmin()
        {
            return _context.Users.DefaultIfEmpty(null).SingleOrDefault(u => u.UserTypeId == 1);
        }
        #endregion
    }
}