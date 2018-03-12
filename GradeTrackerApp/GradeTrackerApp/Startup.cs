using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(GradeTrackerApp.Startup))]
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