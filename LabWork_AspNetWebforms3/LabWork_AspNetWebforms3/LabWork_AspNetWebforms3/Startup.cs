using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabWork_AspNetWebforms3.Startup))]
namespace LabWork_AspNetWebforms3
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
