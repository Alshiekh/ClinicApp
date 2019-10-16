using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicApp.Web.Startup))]
namespace ClinicApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
