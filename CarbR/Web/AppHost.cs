using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using System.Configuration;

namespace Web
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Carb Services", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[]
            //{
            //        new BasicAuthProvider()
            //}));

            //Plugins.Add(new RegistrationFeature());

            //container.Register<ICacheClient>(new MemoryCacheClient());
            //var userRepo = new InMemoryAuthRepository();
            //container.Register<IUserAuthRepository>(userRepo);

            //new SaltedHash().GetHashAndSaltString("Gruppe6!", out string Salt, out string Hash);
            //userRepo.CreateUserAuth(new UserAuth
            //{
            //    FullName = "CarbAdmin Damp",
            //    Email = "CarbAdmin@Carb.dk",
            //    UserName = "CarbAdmin",
            //    Salt = Salt,
            //    PasswordHash = Hash,
            //}, "Gruppe6!");
        }
    }
}