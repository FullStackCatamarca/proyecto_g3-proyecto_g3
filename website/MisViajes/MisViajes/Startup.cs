using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MisViajes.Startup))]
namespace MisViajes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
