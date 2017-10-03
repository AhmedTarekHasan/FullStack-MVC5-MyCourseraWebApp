using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CommonUtilities;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        #region Fields
        private ApplicationDbContext _context;
        #endregion

        #region Constructors
        public CoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region ICoursesRepository Implementations
        public PagedCollection<UserCourseDto> GetCoursesForUserView(string userId, bool showMyAttendingOnly, bool showMyInstructingOnly, string searchTerm, int pageSize = int.MaxValue, int pageIndex = 0)
        {
            PagedCollection<UserCourseDto> result = new PagedCollection<UserCourseDto>();

            var courseAttendersStatus =
            (
                from course in _context.Courses.Where(c => c.IsCancelled == false && (string.IsNullOrEmpty(searchTerm) || c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)))
                join attender in _context.CoursesAttenders on new
                {
                    CourseId = course.Id,
                    UserId = userId
                } equals new
                {
                    CourseId = attender.CourseId,
                    UserId = attender.AttenderId
                } into courseAttenders

                from courseAttender in courseAttenders.DefaultIfEmpty()
                group courseAttender by new
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    From = course.From,
                    To = course.To,
                    IsCancelled = course.IsCancelled
                } into courseAttendersGroup
                select new UserCourseDto
                {
                    CourseId = courseAttendersGroup.Key.CourseId,
                    Title = courseAttendersGroup.Key.Title,
                    Description = courseAttendersGroup.Key.Description,
                    From = courseAttendersGroup.Key.From,
                    To = courseAttendersGroup.Key.To,
                    IsCancelled = courseAttendersGroup.Key.IsCancelled,
                    UserIsCourseAttender = courseAttendersGroup.Any(ca => ca.AttenderId == userId),
                    UserIsCourseInstructor = true
                }
            );

            var courseInstructorsStatus =
                (
                    from course in _context.Courses.Where(c => c.IsCancelled == false && (string.IsNullOrEmpty(searchTerm) || c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)))
                    join instructor in _context.CoursesInstructors on new
                    {
                        CourseId = course.Id,
                        UserId = userId
                    } equals new
                    {
                        CourseId = instructor.CourseId,
                        UserId = instructor.InstructorId
                    } into courseInstructors

                    from courseInstructor in courseInstructors.DefaultIfEmpty()
                    group courseInstructor by new
                    {
                        CourseId = course.Id,
                        Title = course.Title,
                        Description = course.Description,
                        From = course.From,
                        To = course.To,
                        IsCancelled = course.IsCancelled
                    } into courseInsturctorsGroup
                    select new UserCourseDto
                    {
                        CourseId = courseInsturctorsGroup.Key.CourseId,
                        Title = courseInsturctorsGroup.Key.Title,
                        Description = courseInsturctorsGroup.Key.Description,
                        From = courseInsturctorsGroup.Key.From,
                        To = courseInsturctorsGroup.Key.To,
                        IsCancelled = courseInsturctorsGroup.Key.IsCancelled,
                        UserIsCourseAttender = true,
                        UserIsCourseInstructor = courseInsturctorsGroup.Any(ca => ca.InstructorId == userId)
                    }
                );

            var compiled =
                (
                    from attendersStatus in courseAttendersStatus
                    join instructorsStatus in courseInstructorsStatus on attendersStatus.CourseId equals instructorsStatus.CourseId
                    select new UserCourseDto
                    {
                        CourseId = attendersStatus.CourseId,
                        Title = attendersStatus.Title,
                        Description = attendersStatus.Description,
                        From = attendersStatus.From,
                        To = attendersStatus.To,
                        IsCancelled = attendersStatus.IsCancelled,
                        UserIsCourseAttender = attendersStatus.UserIsCourseAttender && instructorsStatus.UserIsCourseAttender,
                        UserIsCourseInstructor = attendersStatus.UserIsCourseInstructor && instructorsStatus.UserIsCourseInstructor
                    }
                );

            if (showMyAttendingOnly)
            {
                compiled = compiled.Where(r => r.UserIsCourseAttender == true && r.UserIsCourseInstructor == false);
            }
            else if (showMyInstructingOnly)
            {
                compiled = compiled.Where(r => r.UserIsCourseInstructor == true && r.UserIsCourseAttender == false);
            }

            PagerToken token = Pager.Page(pageSize, pageIndex, compiled.Count());

            result = new PagedCollection<UserCourseDto>(compiled.OrderBy(c => c.Title).Skip(token.PageFirstItemNumber - 1).Take(token.PageLastItemNumber - token.PageFirstItemNumber + 1).ToList());
            result.PagerToken = token;

            return result;
        }
        public Course GetCourseById(int courseId)
        {
            Course result = null;

            if (courseId > 0)
            {
                result = _context.Courses.Include(c => c.CoursesInstructors).DefaultIfEmpty(null).Single(c => c.Id == courseId);
            }

            return result;
        }
        public void CreateCourse(Course newCourse, string instructorId)
        {
            if (newCourse != null)
            {
                newCourse.CoursesInstructors.Add(new Models.CourseInstructor() { Course = newCourse, InstructorId = instructorId });
                newCourse = _context.Courses.Add(newCourse);
            }
        }
        public bool CheckCourseTitleAlreadyExists(string title)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(title))
            {
                result = _context.Courses.Any(c => c.IsCancelled == false && c.Title.ToLower() == title.ToLower());
            }

            return result;
        }
        public bool MarkCourseAsAttending(int courseId, string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                if (!_context.CoursesAttenders.Any(at => at.CourseId == courseId && at.AttenderId == userId))
                {
                    _context.CoursesAttenders.Add(new Models.CourseAttender()
                    {
                        AttenderId = userId,
                        CourseId = courseId
                    });

                    result = true;
                }
            }

            return result;
        }
        public bool MarkCourseAsNotAttending(int courseId, string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                CourseAttender attender = _context.CoursesAttenders
                    .DefaultIfEmpty(null)
                    .SingleOrDefault(at => at.CourseId == courseId && at.AttenderId == userId);

                if (attender != null)
                {
                    _context.CoursesAttenders.Remove(attender);
                    result = true;
                }
            }

            return result;
        }
        public bool CancelCourse(int courseId, string instructorId)
        {
            bool result = false;

            Course courseToCancel = GetCourseById(courseId);

            if (courseToCancel != null && courseToCancel.CoursesInstructors.Any(i => i.InstructorId == instructorId))
            {
                courseToCancel.Cancel();
                result = true;
            }

            return result;
        }
        #endregion
    }
}