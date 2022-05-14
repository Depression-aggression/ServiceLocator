using System;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Manual.Implementation
{
    public abstract class ServiceRegistrar : IServiceRegistrar
    {
        protected ServiceContainer Container { get; }

        public abstract object RegisterSingle(Type serviceType, object serviceInstance, bool throwError);

        protected ServiceRegistrar(ServiceContainer container)
        {
            Container = container;
        }
    }
}