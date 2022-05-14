namespace Depra.DI.Services.Runtime.Interfaces
{
    public interface IGlobalService<T> : IService
    {
        void InvokeInitializationCallback();
    }
}