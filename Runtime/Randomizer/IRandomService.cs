using Depra.DI.Services.Runtime.Interfaces;

namespace Depra.DI.Services.Runtime.Randomizer
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}