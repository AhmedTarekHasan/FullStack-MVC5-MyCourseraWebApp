using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class AdminMessageNotification : Notification
    {
        #region Constructors
        protected AdminMessageNotification() : base()
        {
            Message = string.Empty;
        }
        public AdminMessageNotification(string message, ApplicationUser byWhom, string byWhomId) : base(byWhom, byWhomId)
        {
            Message = message;
        }
        #endregion

        #region Properties
        public string Message { get; private set; }
        /*public override NotificationTypes Type
        {
            get
            {
                return NotificationTypes.AdminMessage;
            }
        }*/
        #endregion
    }
}