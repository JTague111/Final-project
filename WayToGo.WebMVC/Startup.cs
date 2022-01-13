using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WayToGo.WebMVC.Startup))]
namespace WayToGo.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
