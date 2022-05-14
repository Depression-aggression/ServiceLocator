using System;
using System.Collections.Generic;
using System.Linq;

namespace Depra.DI.Services.Runtime.Containers.Manual.Implementation
{
    public class ManualServiceResolver : ServiceResolver
    {
        public override object ResolveInstance(Type serviceType, bool throwError = true, bool requiresNew = false)
        {
            try
            {
                object instance = null;
                if (Container.DefaultServices.Any(serviceType.IsEquivalentTo))
                {
                    instance = this;
                }

                if (instance == null)
                {
                    Container.Services.TryGetValue(serviceType, out instance);
                }

                if (instance is ServiceCreatorCallback callback)
                {
                    instance = callback(Container, serviceType);
                    if (instance != null && instance.GetType().IsCOMObject == false &&
                        serviceType.IsInstanceOfType(instance) == false)
                    {
                        instance = null;
                    }

                    Container.Services[serviceType] = instance;
                }

                return instance;
            }
            catch (KeyNotFoundException)
            {
                if (throwError)
                {
                    throw new ApplicationException("The requested service is not registered");
                }

                return default;
            }
        }

        public override TService ResolveInstance<TService>(bool throwError = true, bool requiresNew = false) =>
            (TService)ResolveInstance(typeof(TService), throwError, requiresNew);

        public ManualServiceResolver(ServiceContainer container) : base(container)
        {
        }
    }
}