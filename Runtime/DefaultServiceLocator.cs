using Rossoforge.Core.Services;
using Rossoforge.Utils.Logger;
using System;
using System.Collections.Generic;

namespace Rossoforge.Services
{
    public class DefaultServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> services = new();
        private readonly object _lock = new();

        private readonly List<IInitializable> _initializables = new();
        private readonly List<IUpdatable> _updatables = new();
        private readonly List<IFixedUpdatable> _fixedUpdatables = new();
        private readonly List<ILateUpdatable> _lateUpdatables = new();

        public void Initialize()
        {
            for (int i = 0; i < _initializables.Count; i++)
            {
                _initializables[i].Initialize();
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

                if (service is IInitializable initializableService)
                    _initializables.Add(initializableService);

                if (service is IUpdatable updatableService)
                    _updatables.Add(updatableService);

                if (service is IFixedUpdatable fixedUpdatableService)
                    _fixedUpdatables.Add(fixedUpdatableService);

                if (service is ILateUpdatable lateUpdatableService)
                    _lateUpdatables.Add(lateUpdatableService);

                RossoLogger.Info($"Service {key.Name} registered");
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

                RossoLogger.Info($"Service {key.Name} unregistered");
            }
        }

        public void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
            {
                _fixedUpdatables[i].FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
            {
                _lateUpdatables[i].LateUpdate();
            }
        }
    }
}
