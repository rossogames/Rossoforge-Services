using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RossoForge.Service
{
    public class ServiceLocator : IDisposable
    {
        private readonly Dictionary<Type, IService> services = new();
        private readonly List<Type> _initializedServices = new();
        private static ServiceLocator _current;
        private ServiceLocatorProfile _profile;

        public static ServiceLocator Current
        {
            get
            {
                _current ??= new();
                return _current;
            }
        }

        private ServiceLocator()
        {
        }

        public T Get<T>() where T : IService
        {
            Type key = typeof(T);
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"{key.Name} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }

        public void Register<T>(T service) where T : IService
        {
            Type key = typeof(T);
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key.Name} which is already registered with the {GetType().Name}.");
                return;
            }

            services.Add(key, service);
        }

        public void Unregister<T>() where T : IService
        {
            Unregister(typeof(T));
        }
        public void Initialize(ServiceLocatorProfile profile)
        {
            _profile = profile;

            foreach (var item in services)
            {
                if (!_initializedServices.Contains(item.Key))
                {
                    if (item.Value is IInitializable initializableService)
                        initializableService.Initialize();

                    _initializedServices.Add(item.Key);
                }
            }

#if UNITY_EDITOR
            RefreshProfile();
#endif
        }

        public void Dispose()
        {
            var serviceToUnregist = services.Keys.AsEnumerable().ToArray();
            foreach (var service in serviceToUnregist)
                Unregister(service);

#if UNITY_EDITOR
            RefreshProfile();
#endif
        }

        private void Unregister(Type serviceType)
        {
            if (!services.ContainsKey(serviceType))
            {
                Debug.LogError($"Attempted to unregister service of type {serviceType.Name} which is not registered with the {GetType().Name}.");
                return;
            }

            var service = services[serviceType];
            if (service is IDisposable disposableService)
                disposableService.Dispose();

            services.Remove(serviceType);
            _initializedServices.Remove(serviceType);

#if UNITY_EDITOR
            RefreshProfile();
#endif
        }

#if UNITY_EDITOR
        private void RefreshProfile()
        {
            _profile.InitializedServices = _initializedServices.Select(t => t.Name).ToArray();
        }
#endif
    }
}