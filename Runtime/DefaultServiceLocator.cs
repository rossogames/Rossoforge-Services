using Rossoforge.Core.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rossoforge.Services
{
    public class DefaultServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> services = new();
        private readonly object _lock = new();

        public void Initialize()
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

        public T Get<T>() where T : IService
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

        public bool TryGet<T>(out T service) where T : IService
        {
            lock (_lock)
            {
                if (services.TryGetValue(typeof(T), out var result))
                {
                    service = (T)result;
                    return true;
                }
                service = default;
                return false;
            }
        }

        public void Register<T>(T service) where T : IService
        {
            lock (_lock)
            {
                Type key = typeof(T);
                if (services.ContainsKey(key))
                {
                    throw new InvalidOperationException($"Attempted to register service of type {key.Name} which is already registered on {GetType().Name}");
                }

                services.Add(key, service);
#if UNITY_EDITOR
                Debug.Log($"Service {key.Name} registered");
#endif
            }
        }

        public void Unregister<T>() where T : IService
        {
            lock (_lock)
            {
                Type key = typeof(T);
                if (!services.ContainsKey(key))
                {
                    throw new InvalidOperationException($"Attempted to unregister service of type {key.Name} which is not registered on {GetType().Name}");
                }

                var service = services[key];
                if (service is IDisposable disposableService)
                    disposableService.Dispose();

                services.Remove(key);

#if UNITY_EDITOR
                Debug.Log($"Service {key.Name} unregistered");
#endif
            }
        }
    }
}
