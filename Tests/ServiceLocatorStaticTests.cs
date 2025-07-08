using NUnit.Framework;
using System;

namespace Rossoforge.Services.Tests
{
    [TestFixture]
    public class ServiceLocatorStaticTests
    {
        [SetUp]
        public void SetUp()
        {
            ServiceLocator.SetLocator(new DefaultServiceLocator());
        }

        [Test]
        public void Initialize_InitializesServices()
        {
            var initializable = new InitializableService();
            ServiceLocator.Register(initializable);

            ServiceLocator.Initialize();

            Assert.IsTrue(initializable.Initialized);
        }

        [Test]
        public void Get_WorksThroughStaticFacade()
        {
            var service = new DummyService();

            ServiceLocator.Register(service);

            var retrieved = ServiceLocator.Get<DummyService>();

            Assert.AreSame(service, retrieved);
        }

        [Test]
        public void Get_ThrowsWhenServiceNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceLocator.Get<DummyService>());
        }

        [Test]
        public void TryGet_ReturnsTrueWhenRegistered()
        {
            var service = new DummyService();
            ServiceLocator.Register(service);

            var result = ServiceLocator.TryGet<DummyService>(out var retrieved);

            Assert.IsTrue(result);
            Assert.AreSame(service, retrieved);
        }

        [Test]
        public void Unregister_RemovesService()
        {
            var service = new DummyService();
            ServiceLocator.Register(service);

            ServiceLocator.Unregister<DummyService>();

            Assert.Throws<InvalidOperationException>(() => ServiceLocator.Get<DummyService>());
        }

        [Test]
        public void SetLocator_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ServiceLocator.SetLocator(null));
        }
    }
}
