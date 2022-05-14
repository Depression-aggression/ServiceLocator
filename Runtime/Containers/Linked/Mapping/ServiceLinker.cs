using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation;
using Depra.DI.Services.Runtime.Interfaces;
using Depra.DI.Services.Runtime.Providing.Linked.Initialization;
using Depra.DI.Services.Runtime.Providing.Linked.Links;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Interfaces;

namespace Depra.DI.Services.Runtime.Providing.Linked.Mapping
{
    public class ServiceLinker : IServiceLinker, IDisposable
    {
        protected ServiceContainer Container { get; }
        protected NamedServiceContainer NamedContainer { get; }

        public IServiceLink<TInterface> For<TInterface>()
        {
            var interfaceType = typeof(TInterface);
            Container.AddService(interfaceType, new ServiceLink<TInterface>());

            return Container.GetService(interfaceType) as IServiceLink<TInterface>;
        }

        public IServiceLink<TInterface> For<TInterface>(string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return For<TInterface>();
            }

            var type = typeof(TInterface);

            if (NamedContainer.HasService(type))
            {
                NamedContainer.AddService(type, new ServiceLink<TInterface>(), instanceName);
            }
            else
            {
                NamedContainer.AddServices(type,
                    new Dictionary<string, object>() { { instanceName, new ServiceLink<TInterface>() } });
            }

            return NamedContainer.GetService(type, instanceName) as IServiceLink<TInterface>;
        }

        public IObjectLink Use<TObject>(bool lazyInstance = true, bool threadScope = true) where TObject : class, new()
        {
            var objectType = typeof(TObject);
            Container.AddService(objectType, new ObjectLink<TObject>());

            var link = Container.GetService(objectType) as IObjectLink;
            link?.Use(lazyInstance, threadScope);

            return link;
        }

        public IInjectionLink Use<TObject>(IInitializationDependency dependencies, bool threadScope = true)
            where TObject : class, new()
        {
            var objectType = typeof(TObject);
            Container.AddService(objectType, new InjectionLink<TObject>());

            var link = Container.GetService(objectType) as IInjectionLink;
            link?.Use(dependencies, threadScope);

            return link;
        }

        public ServiceLinker(ServiceContainer container, NamedServiceContainer namedContainer)
        {
            Container = container;
            NamedContainer = namedContainer;
        }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}