using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class NotificationDetail
    {
        #region Constructors
        protected NotificationDetail()
        {

        }
        public NotificationDetail(int notificationId, string fieldName, string oldValue, string newValue)
        {
            NotificationId = notificationId;
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public int NotificationId { get; private set; }
        public CourseUpdatedNotification Notification { get; private set; }
        public string FieldName { get; private set; }
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }
        #endregion
    }
}