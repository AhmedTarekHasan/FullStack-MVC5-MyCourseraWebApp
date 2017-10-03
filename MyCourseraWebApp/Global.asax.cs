using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Hubs;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications;
using MyCourseraWebApp.Notifications.NotificationsAntennas;
using MyCourseraWebApp.Repositories;

namespace MyCourseraWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new CustomUserIdProvider());

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContextHelpers.GlobalNotificationsManager.StartListening();
        }
    }
}
