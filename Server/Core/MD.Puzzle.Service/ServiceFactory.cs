using MD.Puzzle.Service.Contracts;
using SimpleInjector;

namespace MD.Puzzle.Service
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly Container _container;

        public ServiceFactory(Container container)
        {
            _container = container;
        }

        public TService GetService<TService>()
        {
            return (TService)_container.GetInstance(typeof(TService));
        }
    }
}
