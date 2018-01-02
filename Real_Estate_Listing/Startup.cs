using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Real_Estate_Listing.Startup))]
namespace Real_Estate_Listing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
