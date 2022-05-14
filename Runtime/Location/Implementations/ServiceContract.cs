namespace Depra.DI.Services.Runtime.Location.Implementations
{
    public class ServiceContract
    {
        public bool ThrowError { get; }
        public bool RequiresNew { get; }

        public ServiceContract(bool throwError, bool requiresNew)
        {
            ThrowError = throwError;
            RequiresNew = requiresNew;
        }
        
        internal static ServiceContract Default { get; }

        static ServiceContract()
        {
            Default = new ServiceContract(true, false);
        }
    }
}