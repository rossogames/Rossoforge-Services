using System;
using System.Collections.Generic;
using UnityEngine;

namespace RossoForge.Service
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> services = new();
        private static ServiceLocator _current;

        private readonly object _lock = new();

        public static void SetLocator(ServiceLocator locator)
        {
            _current = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        public static void Initialize() => _current.InitializeServices();
        public static T Get<T>() where T : IService => _current.GetService<T>();
        public static void Register<T>(T service) where T : IService => _current.RegisterService<T>(service);
        public static void Unregister<T>() where T : IService => _current.UnregisterService<T>();

        private void InitializeServices()
        {
            foreach (var item in services)
            {
                if (item.Value is IInitializable initializableService)
                    initializableService.Initialize();
#if UNITY_EDITOR
                Debug.Log($"Service {item.Key.Name} initialized");
#endif 
            }
        }
        private T GetService<T>() where T : IService
        {
            lock (_lock)
            {
                Type key = typeof(T);
                if (!services.ContainsKey(key))
                {
                    throw new InvalidOperationException($"{key.Name} is not registered on {GetType().Name}");
                }

                return (T)services[key];
            }
        }
        private void RegisterService<T>(T service) where T : IService
        {
            lock (_lock)
            {
                Type _key = typeof(T);
                if (services.ContainsKey(_key))
                {
                    throw new InvalidOperationException($"Attempted to register service of type {_key.Name} which is already registered on {GetType().Name}");
                }

                services.Add(_key, service);
#if UNITY_EDITOR
                Debug.Log($"Service {_key.Name} registered");
#endif
            }
        }

        private void UnregisterService<T>() where T : IService
        {
            lock (_lock)
            {
                Type _key = typeof(T);
                if (!services.ContainsKey(_key))
                {
                    throw new InvalidOperationException($"Attempted to unregister service of type {_key.Name} which is not registered on {GetType().Name}");
                }

                var service = services[_key];
                if (service is IDisposable disposableService)
                    disposableService.Dispose();

                services.Remove(_key);

#if UNITY_EDITOR
                Debug.Log($"Service {_key.Name} unregistered");
#endif
            }
        }
    }
}