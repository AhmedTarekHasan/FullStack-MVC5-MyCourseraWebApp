using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    /*public class NotificationType
    {
        #region Constructors
        protected NotificationType()
        {

        }
        public NotificationType(string name) : this(name, null)
        {

        }
        public NotificationType(string name, string description)
        {
            Name = name;
            Description = description;

            Notifications = new List<Notification>();
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<Notification> Notifications { get; private set; }
        #endregion
    }*/

    public enum NotificationTypes
    {
        AdminMessage = 0,
        CourseCreated = 1,
        CourseUpdated = 2,
        CourseCancelled  = 3
    }
}