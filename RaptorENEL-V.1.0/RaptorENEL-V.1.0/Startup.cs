using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RaptorENEL_V._1._0.Startup))]
namespace RaptorENEL_V._1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
