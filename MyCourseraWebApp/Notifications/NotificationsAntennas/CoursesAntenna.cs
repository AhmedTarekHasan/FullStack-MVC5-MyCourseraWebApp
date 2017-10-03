using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Notifications.NotificationsAntennas
{
    public class CourseUpdateToken
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class CoursesAntenna : ICoursesAntenna
    {
        public event CourseCreatedDelegate CourseCreated;
        public event CourseUpdatedDelegate CourseUpdated;
        public event CourseCancelledDelegate CourseCancelled;

        public void OnCourseCreated(Course course)
        {
            if (CourseCreated != null)
            {
                CourseCreated(course);
            }
        }
        public void OnCourseUpdated(Course course, List<CourseUpdateToken> tokens)
        {
            if (CourseUpdated != null)
            {
                CourseUpdated(course, tokens);
            }
        }
        public void OnCourseCancelled(Course course)
        {
            if (CourseCancelled != null)
            {
                CourseCancelled(course);
            }
        }
    }
}