using System;

namespace Depra.DI.Services.Runtime.Containers.Manual.Implementation
{
    public class ManualServiceRegistrar : ServiceRegistrar
    {
        public override object RegisterSingle(Type serviceType, object serviceInstance, bool throwError)
        {
            if (serviceType == null && throwError)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (serviceInstance == null && throwError)
            {
                throw new ArgumentNullException(nameof(serviceInstance));
            }

            if (serviceInstance is not ServiceCreatorCallback &&
                serviceInstance.GetType().IsCOMObject == false &&
                serviceType.IsInstanceOfType(serviceInstance) == false && throwError)
            {
                throw new ArgumentException($"ErrorInvalidServiceInstance {serviceType.FullName}");
            }

            if (Container.Services.ContainsKey(serviceType) && throwError)
            {
                throw new ApplicationException("Service already registered!");
            }

            Container.Services[serviceType] = serviceInstance;

            return serviceInstance;
        }

        public ManualServiceRegistrar(ServiceContainer container) : base(container)
        {
        }
    }
}