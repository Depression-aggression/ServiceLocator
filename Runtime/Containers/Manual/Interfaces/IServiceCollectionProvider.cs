using System;
using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Containers.Manual.Interfaces
{
    public interface IServiceCollectionProvider
    {
        bool HasService(Type serviceType);
        
        internal IEnumerable<object> GetAllServices();
    }
}