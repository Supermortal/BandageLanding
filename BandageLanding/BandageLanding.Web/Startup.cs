using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BandageLanding.Startup))]
namespace BandageLanding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
