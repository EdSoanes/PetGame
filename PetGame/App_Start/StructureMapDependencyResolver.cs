using System.Collections.Generic;
using System;
using System.Linq;
using StructureMap;

namespace PetGame
{
    public class StructureMapDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            // This example does not support child scopes
            return this;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
                return GetInterfaceService(serviceType);
            return GetConcreteService(serviceType);
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }


        private object GetConcreteService(Type serviceType)
        {
            try
            {
                // Can't use TryGetInstance here because it won’t create concrete types
                return _container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }


        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }
    }
}