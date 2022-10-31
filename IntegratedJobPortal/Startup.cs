using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntegratedJobPortal.Startup))]
namespace IntegratedJobPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
