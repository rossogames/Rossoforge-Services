using NUnit.Framework;
using System;

namespace Rossoforge.Services.Tests
{
    [TestFixture]
    public class DefaultServiceLocatorTest
    {
        private DefaultServiceLocator _locator;

        [SetUp]
        public void SetUp()
        {
            _locator = new DefaultServiceLocator();
        }

        [Test]
        public void Get_Service_ReturnsSameInstance()
        {
            var service = new DummyService();
            _locator.Register(service);

            var retrieved = _locator.Get<DummyService>();
            Assert.AreSame(service, retrieved);
        }

        [Test]
        public void TryGet_ReturnsTrueWhenRegistered()
        {
            var service = new DummyService();
            _locator.Register(service);

            var result = _locator.TryGet<DummyService>(out var retrieved);
            Assert.IsTrue(result);
            Assert.AreSame(service, retrieved);
        }

        [Test]
        public void TryGet_ReturnsFalseWhenNotRegistered()
        {
            var result = _locator.TryGet<DummyService>(out var retrieved);
            Assert.IsFalse(result);
            Assert.IsNull(retrieved);
        }

        [Test]
        public void Get_ThrowsWhenNotRegistered()
        {
            Assert.Throws<InvalidOperationException>(() => _locator.Get<DummyService>());
        }

        [Test]
        public void Register_Duplicate_Throws()
        {
            var service = new DummyService();
            _locator.Register(service);

            var anotherService = new DummyService();
            Assert.Throws<InvalidOperationException>(() => _locator.Register(anotherService));
        }

        [Test]
        public void Unregister_RemovesService()
        {
            var service = new DummyService();
            _locator.Register(service);

            _locator.Unregister<DummyService>();

            Assert.Throws<InvalidOperationException>(() => _locator.Get<DummyService>());
        }

        [Test]
        public void Unregister_NotRegistered_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => _locator.Unregister<DummyService>());
        }

        [Test]
        public void Initialize_CallsInitializableServices()
        {
            var initializable = new InitializableService();
            _locator.Register(initializable);

            _locator.Initialize();

            Assert.IsTrue(initializable.Initialized);
        }

        [Test]
        public void Unregister_DisposesDisposableService()
        {
            var disposable = new DisposableService();
            _locator.Register(disposable);

            _locator.Unregister<DisposableService>();

            Assert.IsTrue(disposable.Disposed);
        }
    }
}