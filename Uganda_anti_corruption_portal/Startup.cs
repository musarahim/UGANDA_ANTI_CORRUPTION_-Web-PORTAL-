using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Uganda_anti_corruption_portal.Startup))]
namespace Uganda_anti_corruption_portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
