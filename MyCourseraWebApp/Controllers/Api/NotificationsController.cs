using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Filters;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        #region Fields
        private IUnitOfWork unitOfWork = null;
        #endregion

        #region Constructors
        public NotificationsController()
        {
            unitOfWork = new UnitOfWork(new Models.ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
        }
        public NotificationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        [Route("api/notifications")]
        [HttpGet]
        public IHttpActionResult GetUserNotifications()
        {
            return Ok(unitOfWork.NotificationsRepository.GetUserNotifications(User.Identity.GetUserId()));
        }

        [Route("api/notifications/seen/{notificationId}")]
        [HttpPost]
        public IHttpActionResult MarkUserNotificationAsSeen(int notificationId)
        {
            IHttpActionResult result = null;
            bool done = false;

            if (notificationId > 0)
            {
                done = unitOfWork.NotificationsRepository.MarkUserNotificationAsSeen(User.Identity.GetUserId(), notificationId);
            }

            if (done)
            {
                unitOfWork.Complete();
                result = Ok(true);
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        [Route("api/notifications/unseen/{notificationId}")]
        [HttpPost]
        public IHttpActionResult MarkUserNotificationAsUnSeen(int notificationId)
        {
            IHttpActionResult result = null;
            bool done = false;

            if (notificationId > 0)
            {
                done = unitOfWork.NotificationsRepository.MarkUserNotificationAsUnSeen(User.Identity.GetUserId(), notificationId);
            }

            if (done)
            {
                unitOfWork.Complete();
                result = Ok(true);
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }

        [Route("api/notifications/dismiss/{notificationId}")]
        [HttpPost]
        public IHttpActionResult MarkUserNotificationAsDismissed(int notificationId)
        {
            IHttpActionResult result = null;
            bool done = false;

            if (notificationId > 0)
            {
                done = unitOfWork.NotificationsRepository.MarkUserNotificationAsDismissed(User.Identity.GetUserId(), notificationId);
            }

            if (done)
            {
                unitOfWork.Complete();
                result = Ok(true);
            }
            else
            {
                result = BadRequest();
            }

            return result;
        }
    }
}
