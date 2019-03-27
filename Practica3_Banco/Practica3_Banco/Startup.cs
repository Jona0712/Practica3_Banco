using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practica3_Banco.Startup))]
namespace Practica3_Banco
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
