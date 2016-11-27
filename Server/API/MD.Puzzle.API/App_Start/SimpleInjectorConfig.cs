using MD.Puzzle.Data;
using MD.Puzzle.Data.Contracts;
using MD.Puzzle.Service;
using MD.Puzzle.Service.Contracts;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace MD.Puzzle.API.App_Start
{
    public static class SimpleInjectorConfig
    {
        public static void RegisterComponents()
        {
            var container = new Container();

            container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            //container.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            #region Shared Utilities

            container.RegisterWebApiRequest<IServiceFactory, ServiceFactory>();
            container.RegisterWebApiRequest<IBookingService, BookingService>();
            container.RegisterWebApiRequest<IFileRepository, FileRepository>();

            #endregion Shared Utilities

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}