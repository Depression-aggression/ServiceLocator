using System;

namespace Depra.DI.Services.Runtime.Containers.Manual.Interfaces
{
    public interface IServiceResolver
    {
        object ResolveInstance(Type serviceType, bool throwError = true, bool requiresNew = false);
    }
}