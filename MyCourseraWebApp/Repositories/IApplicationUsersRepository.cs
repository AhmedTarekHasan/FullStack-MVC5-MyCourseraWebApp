using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Repositories
{
    public interface IApplicationUsersRepository
    {
        ApplicationUser GetUserById(string id);
        ApplicationUser GetSuperAdmin();
    }
}