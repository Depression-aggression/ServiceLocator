using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation
{
    public abstract class NamedServiceRegistrar : INamedServiceRegistrar
    {
        protected NamedServiceContainer Container { get; }
        
        public abstract object RegisterSingle(Type serviceType, object serviceInstance, string serviceName,
            bool throwError);

        public abstract void RegisterMany(Type serviceType, Dictionary<string, object> namedInstances);

        protected NamedServiceRegistrar(NamedServiceContainer container)
        {
            Container = container;
        }
    }
}