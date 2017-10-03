using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyCourseraWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Constructors
        public ApplicationUser()
        {
            CoursesInstructors = new List<CourseInstructor>();
            CoursesAttenders = new List<CourseAttender>();
            Notifications = new List<Notification>();
            UsersNotifications = new List<UserNotification>();
        }
        #endregion

        #region Properties
        public string FullName { get; set; }
        public int UserTypeId { get; set; }
        public virtual ApplicationUserType UserType { get; set; }
        public virtual ICollection<CourseInstructor> CoursesInstructors { get; set; }
        public virtual ICollection<CourseAttender> CoursesAttenders { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserNotification> UsersNotifications { get; set; }
        #endregion

        #region Methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        #endregion
    }
}