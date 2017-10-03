using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class ApplicationUserType
    {
        #region Constructors
        public ApplicationUserType()
        {
            Users = new List<ApplicationUser>();
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        #endregion
    }

    public enum ApplicationUserTypes
    {
        None = 0,
        Manager = 1,
        Student = 2,
        Instructor = 3
    }
}