using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCourseraWebApp.Models
{
    public class Course
    {
        #region Consturctors
        protected Course()
        {
        }
        public Course(string title, string description, DateTime from, DateTime to)
        {
            Title = title;
            Description = description;
            From = from;
            To = to;
            
            CoursesInstructors = new List<CourseInstructor>();
            CoursesAttenders = new List<CourseAttender>();
            CourseCreatedNotifications = new List<CourseCreatedNotification>();
            CourseUpdatedNotifications = new List<CourseUpdatedNotification>();
            CourseCancelledNotifications = new List<CourseCancelledNotification>();
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public bool IsCancelled { get; private set; }
        public ICollection<CourseInstructor> CoursesInstructors { get; set; }
        public ICollection<CourseAttender> CoursesAttenders { get; set; }
        public ICollection<CourseCreatedNotification> CourseCreatedNotifications { get; set; }
        public ICollection<CourseUpdatedNotification> CourseUpdatedNotifications { get; set; }
        public ICollection<CourseCancelledNotification> CourseCancelledNotifications { get; set; }
        #endregion

        #region Methods
        public bool Cancel()
        {
            bool result = false;

            if (!IsCancelled)
            {
                IsCancelled = true;
                result = true;
            }

            return result;
        }
        public bool Update(string description, DateTime from, DateTime to)
        {
            bool result = false;

            if (!IsCancelled)
            {
                Description = description;
                From = from;
                To = to;
                result = true;
            }

            return result;
        }
        public bool CheckHasStarted()
        {
            return (this.From.Date <= DateTime.Today);
        }
        public bool CheckHasEnded()
        {
            return (this.To.Date <= DateTime.Today);
        }
        public bool CheckInProgress()
        {
            return (CheckHasStarted() && !CheckHasEnded());
        }
        #endregion
    }
}