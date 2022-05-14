using System;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces
{
    public interface INamedServiceContainer : INamedServiceCollectionProvider
    {
        object GetService(Type serviceType, string serviceName);

        object AddService(Type serviceType, object serviceInstance, string serviceName);

        void RemoveService(Type serviceType);

        void RemoveService(Type serviceType, string serviceName);
    }
}