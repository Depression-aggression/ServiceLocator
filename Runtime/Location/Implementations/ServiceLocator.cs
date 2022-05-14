using System;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Location.Abstract;
using Depra.DI.Services.Runtime.Containers.Extensions;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Location.Implementations
{
    public class ServiceLocator : ServiceLocator<IServiceContainer>
    {
        public TService RegisterSingle<TService>(TService service) => Container.AddService(service);

        public override object GetService(Type serviceType) => Container.GetService(serviceType);

        public TService GetService<TService>() where TService : new() => Container.GetService<TService>();
        
        public ServiceLocator(IServiceContainer container) : base(container)
        {
        }
    }
}