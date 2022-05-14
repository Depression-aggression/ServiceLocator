using System;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Location.Abstract;
using Depra.DI.Services.Runtime.Containers.Extensions;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Location.Implementations
{
    public class NamedServiceLocator : ServiceLocator<INamedServiceContainer>
    {
        public TService RegisterSingle<TService>(TService service, string serviceName) where TService : new()
        {
            return Container.AddService(service, serviceName);
        }

        public object GetService(Type type, string instanceName)
        {
            return Container.GetService(type, instanceName);
        }

        public TService GetService<TService>(string instanceName) where TService : class
        {
            return Container.GetService<TService>(instanceName);
        }

        public override object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public NamedServiceLocator(INamedServiceContainer container) : base(container)
        {
        }
    }
}