using System;
using System.Collections.Generic;
using Depra.DI.Services.Runtime.Containers.Abstract;
using Depra.DI.Services.Runtime.Containers.Extensions;
using Depra.DI.Services.Runtime.Containers.Manual.Named.Interfaces;

namespace Depra.DI.Services.Runtime.Containers.Manual.Named.Implementation
{
    public class ManualNamedServiceResolver : NamedServiceResolver
    {
        public override object ResolveInstance(Type type, string instanceName, bool throwError = true, bool requiresNew = false)
        {
            try
            {
                var instance = Container.HasService(type, instanceName);
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
        
        public override TService ResolveInstance<TService>(string instanceName, bool throwError = true, bool requiresNew = false)
        {
            try
            {
                var instance = Container.GetService<TService>(instanceName);
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

        public ManualNamedServiceResolver(INamedServiceContainer namedServices) : base(namedServices)
        {
        }
    }
}