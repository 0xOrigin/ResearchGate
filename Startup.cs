using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Research_Gate.Startup))]
namespace Research_Gate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
