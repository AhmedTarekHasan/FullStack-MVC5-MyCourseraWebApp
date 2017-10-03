using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class CourseCreatedNotification : Notification
    {
        #region Constructors
        protected CourseCreatedNotification() : base()
        {

        }
        public CourseCreatedNotification(Course course, int? courseId, ApplicationUser byWhom, string byWhomId) : base(byWhom, byWhomId)
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
        }
        #endregion

        #region Properties
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        /*public override NotificationTypes Type
        {
            get
            {
                return NotificationTypes.CourseCreated;
            }
        }*/
        #endregion
    }
}