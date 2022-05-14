using System;
using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;

namespace Depra.DI.Services.Runtime.Containers
{
    public delegate object ServiceCreatorCallback(IServiceContainer container, Type serviceType);
}