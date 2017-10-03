using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyCourseraWebApp.Filters;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp.Controllers.Api
{
    [Authorize]
    public class CoursesController : ApiController
    {
        #region Fields
        private IUnitOfWork unitOfWork = null;
        #endregion

        #region Constructors
        public CoursesController()
        {
            unitOfWork = new UnitOfWork(new Models.ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
        }
        public CoursesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        [Route("api/courses/attend/{courseId}")]
        [HttpPost]
        public IHttpActionResult MarkCourseAsAttending(int courseId)
        {
            bool result = unitOfWork.CoursesRepository.MarkCourseAsAttending(courseId, User.Identity.GetUserId());

            if (result)
            {
                unitOfWork.Complete();
                return Ok(result);
            }
            else
            {
                return BadRequest("Action is not completed");
            }
        }

        [Route("api/courses/unattend/{courseId}")]
        public IHttpActionResult MarkCourseAsNotAttending(int courseId)
        {
            bool result = unitOfWork.CoursesRepository.MarkCourseAsNotAttending(courseId, User.Identity.GetUserId());

            if (result)
            {
                unitOfWork.Complete();
                return Ok(result);
            }
            else
            {
                return BadRequest("Action is not completed");
            }
        }

        [HttpPost]
        [Route("api/courses/cancel/{courseId}")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public IHttpActionResult Cancel(int courseId)
        {
            bool isCancelled = unitOfWork.CoursesRepository.CancelCourse(courseId, User.Identity.GetUserId());

            if (isCancelled)
            {
                unitOfWork.Complete();

                Course course = unitOfWork.CoursesRepository.GetCourseById(courseId);
                unitOfWork.CoursesAntenna.OnCourseCancelled(course);

                return Ok(isCancelled);
            }
            else
            {
                return BadRequest("Action is not completed");
            }
        }
    }
}
