using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonUtilities;
using MyCourseraWebApp.DTOs;

namespace MyCourseraWebApp.ViewModel
{
    public class ViewUserCoursesViewModel
    {
        #region Constructors
        public ViewUserCoursesViewModel()
        {
            Courses = new List<DTOs.UserCourseDto>();
        }
        #endregion

        #region Properties
        public List<UserCourseDto> Courses { get; private set; }
        public PagerToken PagerToken { get; set; }
        public string SearchTerm { get; set; }
        #endregion
    }
}