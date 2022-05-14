using System;
using Depra.DI.Services.Runtime.Location.Implementations;

namespace Depra.DI.Services.Runtime.Interfaces
{
    /// <summary>
    /// Interface to be implemented by any interface or class that wants to be available via <see cref="ServiceLocator"/>
    /// </summary>
    public interface IService : IDisposable
    {
        bool Initialized { get; }
        bool InitializationAllowed { get; }

        /// <summary>
        /// Used for initialization of service.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Attempts to fetch an initialized instance of the specified service.
        /// </summary>
        /// <param name="obj">Object reference of service.</param>
        /// <returns name="bool">Returns true if an object instance exists, false otherwise.</returns>
        bool TryGetService(out object obj);
    }
    
    public interface IService<T> : IService
    {
        bool TryGetService(out T service);
    }
}