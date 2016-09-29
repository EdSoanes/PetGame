using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.IO;

[assembly: OwinStartup(typeof(PetGame.OAuth.Startup))]

namespace PetGame.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Users
            //Clients
            //Scopes
            var mgr = new InMemoryManager();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(mgr.GetUsers())
                .UseInMemoryClients(mgr.GetClients())
                .UseInMemoryScopes(mgr.GetScopes());

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var options = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "PetGameCert.pfx"), "prolifer8"),
                RequireSsl = false,
                Factory = factory
            };

            app.UseIdentityServer(options);
        }
    }
}
