using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public abstract class Notification
    {
        #region Constructors
        protected Notification()
        {
            When = DateTime.Now;
            UsersNotifications = new List<UserNotification>();
        }
        public Notification(ApplicationUser byWhom, string byWhomId)
        {
            if (byWhom != null)
            {
                ByWhom = byWhom;
            }
            else if (!string.IsNullOrEmpty(byWhomId))
            {
                ByWhomId = byWhomId;
            }
            else
            {
                throw new ArgumentNullException("byWhomId");
            }

            When = DateTime.Now;

            UsersNotifications = new List<UserNotification>();
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public DateTime When { get; private set; }
        public string ByWhomId { get; private set; }
        public ApplicationUser ByWhom { get; private set; }
        //public abstract NotificationTypes Type { get; }
        public ICollection<UserNotification> UsersNotifications { get; private set; }
        #endregion
    }
}