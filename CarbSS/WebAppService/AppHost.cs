using Funq;
using ServiceStack;
using WebAppService.Services;

namespace WebAppService
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost() : base("Services", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {

        }
    }
}