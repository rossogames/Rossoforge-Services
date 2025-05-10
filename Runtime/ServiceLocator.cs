using System;
using System.Collections.Generic;
using UnityEngine;

namespace RossoForge.Service
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> services = new();
        private readonly List<Type> _initializedServices = new();
        private static ServiceLocator _current;

        private ServiceLocator()
        {
            _current = new ServiceLocator();
        }

        public static void Initialize() => _current.InitializeServices();
        public static T Get<T>() where T : IService => _current.GetService<T>();
        public static void Register<T>(T service) where T : IService => _current.RegisterService<T>(service);
        public static void Unregister<T>() where T : IService => _current.UnregisterService<T>();

        private void InitializeServices()
        {
            foreach (var item in services)
            {
                if (!_initializedServices.Contains(item.Key))
                {
                    if (item.Value is IInitializable initializableService)
                        initializableService.Initialize();

                    _initializedServices.Add(item.Key);
                }
            }
        }
        private T GetService<T>() where T : IService
        {
            Type key = typeof(T);
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"{key.Name} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }
        private void RegisterService<T>(T service) where T : IService
        {
            Type key = typeof(T);
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key.Name} which is already registered on {GetType().Name}.");
                return;
            }

            services.Add(key, service);
        }

        private void UnregisterService<T>() where T : IService
        {
            Type _serviceType = typeof(T);
            if (!services.ContainsKey(_serviceType))
            {
                Debug.LogError($"Attempted to unregister service of type {_serviceType.Name} which is not registered on {GetType().Name}.");
                return;
            }

            var service = services[_serviceType];
            if (service is IDisposable disposableService)
                disposableService.Dispose();

            services.Remove(_serviceType);
            _initializedServices.Remove(_serviceType);
        }

        private void DisposeServices()
        {
            //var serviceToUnregist = services.Keys.AsEnumerable().ToArray();
            //foreach (var service in serviceToUnregist)
            //    Unregister(service);
        }
    }
}