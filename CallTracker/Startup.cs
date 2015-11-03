using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CallTracker.Startup))]
namespace CallTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
