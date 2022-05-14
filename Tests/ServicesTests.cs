using Depra.DI.Services.Runtime.Containers.Interfaces;
using Depra.DI.Services.Runtime.Containers.Manual.Implementation;
using Depra.DI.Services.Runtime.Containers.Manual.Interfaces;
using Depra.DI.Services.Runtime.Location.Implementations;
using Depra.Services.Tests;
using NUnit.Framework;

namespace Depra.DI.Services.Tests
{
    public class ServicesTests
    {
        private ServiceLocator _serviceLocator;
        private IServiceContainer _serviceContainer;
        private ServiceTest _service;

        [SetUp]
        public void SetUp()
        {
            var contract = new ServiceContract(false, false);
            var serviceContainer = new ServiceContainer(contract);
            _serviceLocator = new ServiceLocator(serviceContainer);
            _service = new ServiceTest();
        }

        [TearDown]
        public void TearDown()
        {
            _service.Dispose();
            _serviceLocator.Dispose();
        }

        [Test]
        public void Service_Not_Found_In_Empty_Locator()
        {
            Assert.IsNull(_serviceLocator.GetService<ServiceTest>());
        }

        [Test]
        public void Service_Can_Be_Registered_And_Resolved()
        {
            Assert.IsNull(_serviceLocator.GetService<ServiceTest>());

            _serviceLocator.RegisterSingle(_service);

            Assert.IsTrue(_serviceLocator.CanResolve<ServiceTest>());
            Assert.IsNotNull(_serviceLocator.GetService<ServiceTest>());
        }
    }
}