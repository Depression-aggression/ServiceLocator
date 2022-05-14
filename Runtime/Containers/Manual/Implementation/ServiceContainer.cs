using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Collection;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;
using Depra.DI.Services.Runtime.Location.Implementations;
using IServiceProvider = Depra.DI.Services.Runtime.Interfaces.IServiceProvider;

namespace Depra.DI.Services.Runtime.Containers.Manual.Implementation
{
    public class ServiceContainer : IServiceContainer, IDisposable
    {
        private readonly IServiceResolver _resolver;
        private readonly IServiceRegistrar _registrar;
        private readonly IServiceProvider _parentProvider;
        private readonly ServiceContract _contract;

        private ServiceCollection<object> _services;

        private static Type[] _defaultServices = new Type[2]
        {
            typeof(IServiceContainer),
            typeof(ServiceContainer)
        };

        private bool CanThrowError => _contract.ThrowError;
        private bool CanRequiresNew => _contract.RequiresNew;

        internal ServiceCollection<object> Services => _services ??= new ServiceCollection<object>();

        internal Type[] DefaultServices => _defaultServices;

        public bool HasService(Type serviceType) => Services.ContainsKey(serviceType);

        public object AddService(Type serviceType, object serviceInstance) =>
            _registrar.RegisterSingle(serviceType, serviceInstance, CanThrowError);

        public object AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            if (serviceType == null && CanThrowError)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (Services.ContainsKey(serviceType!) && CanThrowError)
            {
                throw new ArgumentException($"ErrorServiceExists {serviceType.FullName} {nameof(serviceType)}");
            }

            Services[serviceType] = callback ?? throw new ArgumentNullException(nameof(callback));

            return callback;
        }

        public void RemoveService(Type serviceType)
        {
            if (serviceType == null && CanThrowError)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Services.Remove(serviceType!);
        }

        public object GetService(Type serviceType)
        {
            var instance = _resolver.ResolveInstance(serviceType, CanThrowError, CanRequiresNew);
            if (instance == null && _parentProvider != null)
            {
                instance = _parentProvider.GetService(serviceType);
            }

            return instance;
        }

        IEnumerable<object> IServiceCollectionProvider.GetAllServices() => Services.Values;

        public ServiceContainer(ServiceContract contract = null)
        {
            _contract = contract ?? ServiceContract.Default;
            _resolver = new ManualServiceResolver(this);
            _registrar = new ManualServiceRegistrar(this);
        }

        public ServiceContainer(IServiceProvider parentProvider, ServiceContract contract = null) : this(contract)
        {
            _parentProvider = parentProvider;
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false)
            {
                return;
            }

            var services = _services;
            _services = null;

            if (services == null)
            {
                return;
            }

            foreach (var obj in services.Values)
            {
                if (obj is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}