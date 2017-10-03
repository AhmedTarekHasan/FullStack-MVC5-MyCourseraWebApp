using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class UserNotification
    {
        #region Constructors
        protected UserNotification()
        {

        }
        public UserNotification(int notificationId, string userId)
        {
            NotificationId = notificationId;
            UserId = userId;
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public int NotificationId { get; private set; }
        public Notification Notification { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }
        public bool IsDismissed { get; private set; }
        public bool IsSeen { get; private set; }
        public bool IsPinned { get; private set; }
        #endregion

        #region Methods
        public bool Dismiss()
        {
            bool result = false;

            if (!IsDismissed)
            {
                IsDismissed = true;
                result = true;
            }

            return result;
        }
        public bool MarkAsSeen()
        {
            bool result = false;

            if (!IsSeen)
            {
                IsSeen = true;
                result = true;
            }

            return result;
        }
        public bool MarkAsUnSeen()
        {
            bool result = false;

            if (IsSeen)
            {
                IsSeen = false;
                result = true;
            }

            return result;
        }
        public bool MarkAsPinned()
        {
            bool result = false;

            if (!IsPinned)
            {
                IsPinned = true;
                result = true;
            }

            return result;
        }
        public bool MarkAsUnPinned()
        {
            bool result = false;

            if (IsPinned)
            {
                IsPinned = false;
                result = true;
            }

            return result;
        }
        #endregion
    }
}