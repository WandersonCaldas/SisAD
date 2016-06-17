using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SisAD.Startup))]
namespace SisAD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
