using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoTI2_servidor.Startup))]
namespace ProjetoTI2_servidor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
