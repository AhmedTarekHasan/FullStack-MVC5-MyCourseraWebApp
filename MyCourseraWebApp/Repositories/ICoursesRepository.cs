using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonUtilities;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Repositories
{
    public interface ICoursesRepository
    {
        PagedCollection<UserCourseDto> GetCoursesForUserView(string userId, bool showMyAttendingOnly, bool showMyInstructingOnly, string searchTerm, int pageSize = int.MaxValue, int pageIndex = 1);
        Course GetCourseById(int courseId);
        void CreateCourse(Course newCourse, string instructorId);
        bool CheckCourseTitleAlreadyExists(string title);
        bool MarkCourseAsAttending(int courseId, string userId);
        bool MarkCourseAsNotAttending(int courseId, string userId);
        bool CancelCourse(int courseId, string instructorId);
    }
}