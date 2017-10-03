using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCourseraWebApp.Startup))]
namespace MyCourseraWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
