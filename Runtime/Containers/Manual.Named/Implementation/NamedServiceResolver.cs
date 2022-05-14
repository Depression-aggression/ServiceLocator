using System;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Abstract
{
    public abstract class NamedServiceResolver : INamedServiceResolver
    {
        protected INamedServiceContainer Container { get; }
        
        public abstract object ResolveInstance(Type type, string instanceName, bool throwError = true,
            bool requiresNew = false);
        
        public abstract TService ResolveInstance<TService>(string instanceName, bool throwError = true,
            bool requiresNew = false);
        
        protected NamedServiceResolver(INamedServiceContainer namedServices)
        {
            Container = namedServices;
        }
    }
}