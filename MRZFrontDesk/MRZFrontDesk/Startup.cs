using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MRZFrontDesk.Startup))]
namespace MRZFrontDesk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
