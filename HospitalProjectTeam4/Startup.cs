using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalProjectTeam4.Startup))]
namespace HospitalProjectTeam4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
