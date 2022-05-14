using System;
using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation
{
    public class ManualNamedServiceRegistrar : NamedServiceRegistrar
    {
        public override object RegisterSingle(Type serviceType, object serviceInstance, string serviceName,
            bool throwError)
        {
            if (Container.HasService(serviceType, serviceName) && throwError)
            {
                throw new ApplicationException("Service already registered!");
            }

            Container.Services[serviceType][serviceName] = serviceInstance;

            return serviceInstance;
        }

        public override void RegisterMany(Type serviceType, Dictionary<string, object> namedInstances)
        {
            Container.Services.Add(serviceType, namedInstances);
        }

        public ManualNamedServiceRegistrar(NamedServiceContainer container) : base(container)
        {
        }
    }
}