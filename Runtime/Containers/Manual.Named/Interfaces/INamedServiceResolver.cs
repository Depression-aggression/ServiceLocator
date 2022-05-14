using System;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces
{
    public interface INamedServiceResolver
    {
        object ResolveInstance(Type type, string instanceName, bool throwError = true, bool requiresNew = false);
    }
}