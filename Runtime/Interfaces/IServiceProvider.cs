using System;

namespace Depra.DI.Services.Runtime.Interfaces
{
    public interface IServiceProvider
    {
        object GetService(Type type);
    }
}