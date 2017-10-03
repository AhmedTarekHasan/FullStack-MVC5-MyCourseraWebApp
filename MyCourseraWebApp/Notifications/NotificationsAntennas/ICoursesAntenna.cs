using System.Collections.Generic;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Notifications.NotificationsAntennas
{
    public delegate void CourseCreatedDelegate(Course course);
    public delegate void CourseUpdatedDelegate(Course course, List<CourseUpdateToken> tokens);
    public delegate void CourseCancelledDelegate(Course course);

    public interface ICoursesAntenna
    {
        event CourseCancelledDelegate CourseCancelled;
        event CourseCreatedDelegate CourseCreated;
        event CourseUpdatedDelegate CourseUpdated;

        void OnCourseCreated(Course course);
        void OnCourseUpdated(Course course, List<CourseUpdateToken> tokens);
        void OnCourseCancelled(Course course);
    }
}