using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Net.Http.Headers;
using System.Web.Http;
using StructureMap;
using PetGame.Services;
using PetGame.Repositories;
using PetGame.Services.Impl;
using PetGame.Repositories.Impl;
using Microsoft.Owin.Security.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(PetGame.App_Start.Startup))]

namespace PetGame.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;

            Register(config);

            //var certificate = new X509Certificate2(Convert.FromBase64String("MIIEsDCCApigAwIBAgIQZggE3TopDa9MsHfIQaz1GDANBgkqhkiG9w0BAQUFADAUMRIwEAYDVQQDEwlsb2NhbGhvc3QwHhcNMTYwOTE5MjMwMDAwWhcNMjYwOTI2MjMwMDAwWjAUMRIwEAYDVQQDEwlsb2NhbGhvc3QwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQDR0lEFPDK4xUgQkVX7VqsdAFrdxY0kBALR6VUXSjXJTBvmish0If2a8dcaYZpmq1aZ4ATA8jSOFKEDf6KYFGxTL9uO6usVhx3JuHN4Xe2gcRlpi2uD7cPyzJ1apKzlLvX2DxlTXDTUZ4FsIdvZMnM3pb1iJP54xhGJg2joM0zxSC0j0zTIGWMStzQ7Ru2IVdIDnVzsFM3YFXZrWHoYyYKcMnw3ScVOIRqA0JziwjLQdQJvH2hLpGkboU5UlBXi76hkE4tNzfjtIw9hyMTIlHoX0dA9wK8IG80LCsdFxgdErNUmoyuuirkdlbVHZhQBr1rWnDyT4D97Em4TvTc/mdlC6iWEAChvPHzl06Ujjy9pLE8XXxRhLLwc04L7IyA6n/l6DM2a7/1t6lJiB1B+TO4u3gavJlZ5x1QgMAO1UgpzQbGOM+JEXb2qYfI7Fd5dLyrZcaoQn5nwoqstMQipqU1AOKiw8cRX6zN8D82I3n1AOyUwLbBm4h6fza8w+pIef90+PBq2GzoaN/MKywMtvQMkCWHZ00FBAeQOLC+N8AZnBkli0ehSJDhkLNkiY6tHuO3d4Fw7P/BNNg53W8+h/WEkoJWNp7bYxp7u1G5xdbNrYfTMGbYbA6Z/ktP/pwz/0C1JBCGGlWUcZJu45TLY4Tk+5Xgsn53cplPmNrFnA7AhVQIDAQABMA0GCSqGSIb3DQEBBQUAA4ICAQCZgAewbeaGIZWyvpWKzV0f65ld3XT1u0hreFrGO1Uilzpwa07PoNVF09wFCJS6zYGpZyYL5f88Vp4hHJJK6YGvkG4za0Dc1CrNKimMuoCFM3A+PajJ7KFWb/Hu4M+XGXD+wxkR26yEPICZcqpWeH1gEGmw/xBLgCK/XVxFex+pBr7GTWl8dLQHmEsMzcQOq4ToIkkdq+JBam76kSvZh4NsPhtWBeYXP8NASuss0HMOadqcfC80TlSC0gZ2iDBCiG1FwhD3QHSriz2QF2EEwkn2TSvROsxy4SoIzZadVpuva8SPVcUX9jiZY2pWkV1EVEuLKwDyPZX3jfhyve/9JXQcexT7XcFogcUHMD4N9qg2sfz7MHrDnhdMV75ICcrOMbOWs8GFmib/08ktDx0asLyuA72t7q3S6iOC8UXjuM5vd/97Vrk3slLsAa9uNmvJI4ZikQSeHfpF3AvkuOf2PAPx9pgEXrE3hh7R3G1EB+7eAqgSiuq33DucPebhKywnZSx8Oa/4q6a139arRuiOhX3uYuyb/V86JpNyaavjg5A4U0ooX71jyEsw3Tq0h7HCCbGB5RcH6fUgz+iPWHmktzxJcue7vQ5gh4be6PnxLo9i7+04kuD89ty4FyCCht4+nxHZezWAUh9igIqEb8YHsA959YUN6CC52ImWzMzdnueUTA=="));
            //app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            //{
            //    AllowedAudiences = new[] { "http://localhost:56385/resources" },
            //    TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidAudience = "http://localhost:56385/resources",
            //        ValidIssuer = "http://localhost:56385",
            //        IssuerSigningKey = new X509SecurityKey(certificate)
            //    }
            //});
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:56385"
            });

            app.UseWebApi(config);

            config.EnsureInitialized();
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new Container(c =>
            {
                c.For<ISysTime>().Use<SysTime>().Singleton();
                c.For<IUserRepository>().Use<UserRepository>().Singleton();
                c.For<IAnimalTypeRepository>().Use<AnimalTypeRepository>().Singleton();
                c.For<IAnimalRepository>().Use<AnimalRepository>().Singleton();
                c.For<IGameService>().Use<GameService>().Singleton();
            });

            config.DependencyResolver = new StructureMapDependencyResolver(container);

            // Web API routes
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.MapHttpAttributeRoutes();
        }
    }
}
