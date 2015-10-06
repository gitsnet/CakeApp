using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CakeApp.Startup))]
namespace CakeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
