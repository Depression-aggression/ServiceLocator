using System;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation;
using Depra.DI.Services.Runtime.Interfaces;
using Depra.DI.Services.Runtime.Providing.Linked.Mapping;

namespace Depra.DI.Services.Runtime.Containers.Linked.Mapping
{
    public class MappedServiceRegistrar : IGenericServiceRegistrar
    {
        private readonly ServiceContainer _container;
        private readonly NamedServiceContainer _namedContainer;
        private readonly IServiceLinker _linker;
        
        public TService RegisterSingle<TService>(TService serviceInstance, bool throwError) where TService : new()
        {
            var serviceType = typeof(TService);
            if (_container.HasService(serviceType) && throwError)
            {
                throw new ApplicationException("Service already registered!");
            }

            _linker.For<TService>().Use<TService>();

            return serviceInstance;
        }

        public TService RegisterSingle<TService>(TService service, string serviceName, bool throwError) where TService : new()
        {
            var serviceType = typeof(TService);
            if (_namedContainer.HasService(serviceType, serviceName) && throwError)
            {
                throw new ApplicationException("Service already registered!");
            }

            _linker.For<TService>(serviceName).Use<TService>();

            return service;
        }

        public MappedServiceRegistrar(ServiceContainer container, NamedServiceContainer namedContainer)
        {
            _container = container;
            _namedContainer = namedContainer;
            _linker = new ServiceLinker(container, namedContainer);
        }
    }
    
    
}