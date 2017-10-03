using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class CourseUpdatedNotification : Notification
    {
        #region Constructors
        protected CourseUpdatedNotification() : base()
        {
            NotificationDetails = new List<NotificationDetail>();
        }
        public CourseUpdatedNotification(Course course, int? courseId, ApplicationUser byWhom, string byWhomId) : base(byWhom, byWhomId)
        {
            if (course != null)
            {
                Course = course;
            }
            else if (courseId.HasValue)
            {
                CourseId = courseId.Value;
            }
            else
            {
                throw new ArgumentNullException("courseId");
            }

            NotificationDetails = new List<NotificationDetail>();
        }
        #endregion

        #region Properties
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        /*public override NotificationTypes Type
        {
            get
            {
                return NotificationTypes.CourseUpdated;
            }
        }*/
        public ICollection<NotificationDetail> NotificationDetails { get; private set; }
        #endregion
    }
}