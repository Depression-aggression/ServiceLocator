using System;
using System.Collections.Generic;
using System.Linq;
using Depra.DI.Services.Runtime.Containers.Collection;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;
using Depra.DI.Services.Runtime.Location.Implementations;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation
{
    public class NamedServiceContainer : INamedServiceContainer, IDisposable
    {
        private readonly INamedServiceResolver _resolver;
        private readonly INamedServiceRegistrar _registrar;
        private readonly ServiceContract _contract;

        private ServiceCollection<Dictionary<string, object>> _services;

        private static Type[] _defaultServices =
        {
            typeof(INamedServiceContainer),
            typeof(NamedServiceContainer)
        };

        internal virtual Type[] DefaultServices => _defaultServices;

        internal ServiceCollection<Dictionary<string, object>> Services =>
            _services ??= new ServiceCollection<Dictionary<string, object>>();

        private bool CanThrowError => _contract.ThrowError;
        private bool CanRequiresNew => _contract.RequiresNew;

        public bool HasService(Type serviceType) => Services.ContainsKey(serviceType);

        public bool HasService(Type serviceType, string serviceName) => Services[serviceType].ContainsKey(serviceName);

        public object AddService(Type serviceType, object serviceInstance, string serviceName) =>
            _registrar.RegisterSingle(serviceType, serviceInstance, serviceName, CanThrowError);

        public void AddServices(Type serviceType, Dictionary<string, object> instanceCollection) =>
            _registrar.RegisterMany(serviceType, instanceCollection);

        public void RemoveService(Type serviceType)
        {
            if (serviceType == null && CanThrowError)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Services.Remove(serviceType!);
        }

        public void RemoveService(Type serviceType, string serviceName)
        {
            if (serviceType == null && CanThrowError)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Services[serviceType!].Remove(serviceName);
        }

        public object GetService(Type serviceType, string serviceName)
        {
            var serviceInstance = _resolver.ResolveInstance(serviceType, serviceName, CanThrowError, CanRequiresNew);
            return serviceInstance;
        }

        IEnumerable<object> IServiceCollectionProvider.GetAllServices() => Services.Values;

        public NamedServiceContainer(ServiceContract contract = null)
        {
            _contract = contract ?? ServiceContract.Default;
            _resolver = new ManualNamedServiceResolver(this);
            _registrar = new ManualNamedServiceRegistrar(this);
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

            foreach (var obj in services.Values.SelectMany(dictionary => dictionary.Values))
            {
                if (obj is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}