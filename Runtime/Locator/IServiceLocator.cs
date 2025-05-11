namespace RossoForge.Service.Locator
{
    public interface IServiceLocator
    {
        void Initialize();
        T Get<T>() where T : IService;
        bool TryGet<T>(out T service) where T : IService;
        void Register<T>(T service) where T : IService;
        void Unregister<T>() where T : IService;
    }
}
