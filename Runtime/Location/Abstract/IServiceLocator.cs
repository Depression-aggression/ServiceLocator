using System;
using System.Collections.Generic;

namespace Depra.DI.Services.Runtime.Location.Abstract
{
    internal interface IServiceLocator
    {
        bool CanResolve(Type serviceType);

        bool CanResolve<TService>();

        IEnumerable<object> GetAllServices();
    }
}