using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Abstract;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;
using Depra.DI.Services.Runtime.Providing.Linked.Links.Abstract;

namespace Depra.DI.Services.Runtime.Containers.Linked.Mapping
{
    public class MappedServiceResolver : ServiceResolver, INamedServiceResolver
    {
        private readonly NamedServiceContainer _namedContainer;

        public override TService ResolveInstance<TService>(bool throwError = true, bool requiresNew = false)
        {
            try
            {
                var serviceType = typeof(TService);
                var link = Container.GetService(serviceType) as GenericLink<TService>;
                if (link.HasInstance == false && link.HasDependencies)
                {
                    return ResolveDependencies(link, requiresNew) is TService
                        ? (TService)ResolveDependencies(link, requiresNew)
                        : default;
                }

                return link.Invoke(requiresNew);
            }
            catch (KeyNotFoundException)
            {
                if (throwError == false)
                {
                    return default;
                }

                throw new ApplicationException("The requested service is not registered!");
            }
        }

        public TService ResolveInstance<TService>(string instanceName, bool throwError,
            bool requiresNew = false)
        {
            try
            {
                var link = _namedContainer.GetService(typeof(TService), instanceName) as GenericLink<TService>;
                if (link.HasInstance == false && link.HasDependencies)
                {
                    return ResolveDependencies(link, requiresNew, instanceName) is TService
                        ? (TService)ResolveDependencies(link, requiresNew, instanceName)
                        : default;
                }

                return link.Invoke(requiresNew);
            }
            catch (KeyNotFoundException)
            {
                if (throwError == false)
                {
                    return default;
                }

                throw new ApplicationException("The requested service is not registered!");
            }
        }

        public override object ResolveInstance(Type serviceType, bool throwError = true, bool requiresNew = false)
        {
            try
            {
                var link = Container.GetService(serviceType) as BaseLink;
                return link.HasDependencies
                    ? ResolveDependencies(link, requiresNew)
                    : link.InvokeObject(requiresNew);
            }
            catch (KeyNotFoundException)
            {
                if (throwError == false)
                {
                    return default;
                }

                throw new ApplicationException("The requested service is not registered");
            }
        }

        public object ResolveInstance(Type serviceType, string instanceName, bool throwError = true,
            bool requiresNew = false)
        {
            try
            {
                var link = _namedContainer.GetService(serviceType, instanceName) as BaseLink;
                return link.HasDependencies
                    ? ResolveDependencies(link, requiresNew, instanceName)
                    : link.InvokeObject(requiresNew);
            }
            catch (KeyNotFoundException)
            {
                if (throwError == false)
                {
                    return default;
                }

                throw new ApplicationException("The requested service is not registered");
            }
        }

        public MappedServiceResolver(ServiceContainer container, NamedServiceContainer namedServices) : 
            base(container)
        {
            _namedContainer = namedServices;
        }

        private object ResolveDependencies(BaseLink link, bool requiresNew, string instanceName = null)
        {
            var parameters = new object[link.Dependencies.Length];

            for (var i = 0; i < link.Dependencies.Length; i++)
            {
                if (HasNamedInstance(link.Dependencies[i], instanceName))
                {
                    var dependency = _namedContainer.GetService(link.Dependencies[i], instanceName) as BaseLink;
                    parameters[i] = dependency.HasDependencies
                        ? ResolveDependencies(dependency, requiresNew, instanceName)
                        : dependency.InvokeObject(requiresNew);
                }
                else if (Container.HasService(link.Dependencies[i]))
                {
                    if (Container.HasService(link.Dependencies[i]) == false)
                    {
                        continue;
                    }

                    var dependency = Container.GetService(link.Dependencies[i]) as BaseLink;
                    parameters[i] = dependency.HasDependencies
                        ? ResolveDependencies(dependency, requiresNew, instanceName)
                        : dependency.InvokeObject(requiresNew);
                }
                else
                {
                    parameters[i] = null;
                }
            }

            return link.InvokeObject(requiresNew, parameters);
        }

        private bool HasNamedInstance(Type type, string instanceName)
        {
            return string.IsNullOrWhiteSpace(instanceName) == false
                   && _namedContainer.HasService(type)
                   && _namedContainer.Services[type].ContainsKey(instanceName);
        }
    }
}