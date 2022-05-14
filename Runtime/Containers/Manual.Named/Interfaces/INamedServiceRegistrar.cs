using System;
using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces
{
    public interface INamedServiceRegistrar
    {
        object RegisterSingle(Type serviceType, object serviceInstance, string serviceName, bool throwError);

        void RegisterMany(Type serviceType, Dictionary<string, object> namedInstances);
    }
}