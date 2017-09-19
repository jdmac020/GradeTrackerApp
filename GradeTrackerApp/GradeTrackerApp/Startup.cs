using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradeTrackerApp.Startup))]
namespace GradeTrackerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
