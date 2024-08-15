using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiAplications.Startup))]

namespace WebApiAplications
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //ConfigureSTSAuth(app);
        }
    }
}
