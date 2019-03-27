using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practica3_Banco_Usac.Startup))]
namespace Practica3_Banco_Usac
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
