using System;

namespace Depra.DI.Services.Runtime.Providing.Linked.Initialization
{
    public class ConstructorDependency : IInitializationDependency
    {
        public Type[] Parameters { get; }
        
        public TObject GetInstance<TObject>(object[] parameters) where TObject : class, new()
        {
            if (parameters == null || parameters.Length == 0)
            {
                return new TObject();
            }
            
            return typeof(TObject).GetConstructor(Parameters)?.Invoke(parameters) as TObject;
        }

        public ConstructorDependency(params Type[] constructorParameters)
        {
            Parameters = constructorParameters;
        }
    }
}