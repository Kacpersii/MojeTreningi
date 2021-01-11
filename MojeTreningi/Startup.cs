using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MojeTreningi.Startup))]
namespace MojeTreningi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
