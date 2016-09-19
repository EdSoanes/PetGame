using PetGame.Repositories;
using PetGame.Repositories.Impl;
using PetGame.Services;
using PetGame.Services.Impl;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PetGame
{
    public static class WebApiConfig
    {
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
