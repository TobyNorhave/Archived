using Funq;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("PlexCore Services", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //Configuration for ServiceStack goes here
        }
    }
}