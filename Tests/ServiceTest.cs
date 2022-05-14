using Depra.DI.Services.Runtime.Interfaces;

namespace Depra.Services.Tests
{
    public class ServiceTest : IService
    {
        public bool Initialized { get; private set; }

        public bool InitializationAllowed => true;
        
        public void Initialize()
        {
            Initialized = true;
        }

        public bool TryGetService(out object obj)
        {
            obj = this;
            return true;
        }
        
        public void Dispose()
        {
            Initialized = false;
        }
    }
}