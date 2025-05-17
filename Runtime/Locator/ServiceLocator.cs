using System;

namespace RossoForge.Services.Locator
{
    public static class ServiceLocator
    {
        private static IServiceLocator _current;

        public static void SetLocator(IServiceLocator locator)
        {
            _current = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        public static void Initialize() => _current.Initialize();
        public static T Get<T>() where T : IService => _current.Get<T>();
        public static void Register<T>(T service) where T : IService => _current.Register<T>(service);
        public static void Unregister<T>() where T : IService => _current.Unregister<T>();
    }
}