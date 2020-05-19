using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CarbWeb
{
    public class Global : System.Web.HttpApplication
    {

        public class CarbV3AppHost : AppHostBase
        {
            public CarbV3AppHost() : base("Carb Service", typeof(CafeService).Assembly)
            { }

            public override void Configure(Container container)
            {
                Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[] 
                {
                    new BasicAuthProvider()
                }));

                Plugins.Add(new RegistrationFeature());

                container.Register<ICacheClient>(new MemoryCacheClient());
                var userRepo = new InMemoryAuthRepository();
                container.Register<IUserAuthRepository>(userRepo);

                new SaltedHash().GetHashAndSaltString("pass", out string hash, out string salt);
                userRepo.CreateUserAuth(new UserAuth
                {
                    Id = 1,
                    DisplayName = "Toby",
                    UserName = "Toby",
                    FirstName = "Tobias",
                    LastName = "K. N",
                    PasswordHash = hash,
                    Salt = salt,
                    Roles = new List<string> {RoleNames.Admin},
                    //Permissions = new List<string> { "GetCafe" },
                }, "pass");
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new CarbV3AppHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}