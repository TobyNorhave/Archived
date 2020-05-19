using Funq;
using ServiceStack.WebHost.Endpoints;
using WebService.Services;

namespace WebService
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Services", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
        }
    }
}