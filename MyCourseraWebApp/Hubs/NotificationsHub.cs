using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using MyCourseraWebApp.Controllers.Api;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp.Hubs
{
    public class NotificationsHub : Hub
    {
        
        private IUnitOfWork unitOfWork = null;

        public NotificationsHub()
        {
            unitOfWork = new UnitOfWork(new Models.ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
        }

        public NotificationsHub(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void GetMyNotifications()
        {
            string userId = Context.User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                Clients.Client(Context.ConnectionId).receiveNotifications(unitOfWork.NotificationsRepository.GetUserNotifications(userId));
            }
        }

        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                ContextHelpers.ConnectedConnections.Add(userId, Context.ConnectionId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = Context.User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                ContextHelpers.ConnectedConnections.Remove(userId, Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                if (!ContextHelpers.ConnectedConnections.GetConnections(userId).Contains(Context.ConnectionId))
                {
                    ContextHelpers.ConnectedConnections.Add(userId, Context.ConnectionId);
                }
            }

            return base.OnReconnected();
        }
    }
}