using System;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Manual.Implementation
{
    public abstract class ServiceResolver : IServiceResolver
    {
        protected ServiceContainer Container { get; }
        
        public abstract object ResolveInstance(Type serviceType, bool throwError = true, bool requiresNew = false);
        
        public abstract TService ResolveInstance<TService>(bool throwError = true, bool requiresNew = false)
            where TService : new();
        
        protected ServiceResolver(ServiceContainer container)
        {
            Container = container;
        }
    }
}