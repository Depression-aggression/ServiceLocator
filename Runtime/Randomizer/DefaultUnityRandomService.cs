using UnityEngine;

namespace Depra.DI.Services.Runtime.Randomizer
{
    public class DefaultUnityRandomService
    {
        public int Next(int minInclusive, int maxExclusive) => Random.Range(minInclusive, maxExclusive);
    }
}