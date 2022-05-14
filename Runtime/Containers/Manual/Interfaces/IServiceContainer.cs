using System;
using IServiceProvider = Depra.DI.Services.Runtime.Interfaces.IServiceProvider;

namespace Depra.DI.Services.Runtime.Containers.Manual.Interfaces
{
    public interface IServiceContainer : IServiceProvider, IServiceCollectionProvider
    {
        object AddService(Type serviceType, object serviceInstance);

        object AddService(Type serviceType, ServiceCreatorCallback callback);
        
        void RemoveService(Type serviceType);
    }
}