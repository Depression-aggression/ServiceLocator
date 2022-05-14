using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation;

namespace Depra.DI.Services.Runtime.Providing.Linked.Mapping
{
    public abstract class ServiceMap : ServiceLinker
    {
        public abstract void Load();

        internal IDictionary<Type, object> Links => Container.Services;

        internal IDictionary<Type, Dictionary<string, object>> NamedLinks => NamedContainer.Services;

        protected ServiceMap(ServiceContainer container, NamedServiceContainer namedContainer) : base(container,
            namedContainer)
        {
        }
    }
}