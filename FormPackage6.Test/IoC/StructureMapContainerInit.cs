using FormPackage6.Core.DomainModel.Base;
using FormPackage6.Core.Repositories.Base;
using FormPackage6.Core.Services.FormServices;
//using FormPackage6.Core.Services.SearchServices;
//using FormPackage6.Core.Services.SerializerServices;
//using FormPackage6.Core.Services.TreeService;
using FormPackage6.Core.Services.UmbracoServices;
using FormPackage6.Core.ValidationHandler.Base;
using FormPackage6.Dispatcher;
using FormPackage6.Infrastructure.DAL.EF;
using FormPackage6.Infrastructure.FormServices;
using FormPackage6.Infrastructure.Mock;
//using FormPackage6.Infrastructure.SerializerServices;
using FormPackage6.Infrastructure.UmbracoServices;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace FormPackage6.Test.IoC
{
    internal class StructureMapContainerInit
    {
        public static IContainer InitilizeTestContainer()
        {
            var container = new Container(c => c.AddRegistry<TestRegistry>());
            return container;
        }
    }

    internal class TestRegistry : MockRegistry
    {
        #region Constructors and Destructors

        public TestRegistry() : base()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AssemblyContainingType<IDomainModel>();
                    scan.AddAllTypesOf<IDomainModel>();
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidationHandler<>));
                });

            Policies.SetAllProperties(prop =>
            {
                prop.OfType<IContainer>();
            });

            For<IValidateBus>().Use<DefaultValidateBus>();
            // Command
            //For<ICommandBus>().Use<DefaultCommandBus>();
            //For<IQueryParser>().Use<DefaultQueryParser>();

            // Unit Of Work
            For<EfUnitOfWork>().Use<EfUnitOfWork>().Named("UnitOfWorkObject");
            For<IUnitOfWork>().Use(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject"));

            For<DbContext>().Singleton().Use<EfUnitOfWork>(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject"));

            //Umbraco Content Service
            //For<UmbracoHelper>().Use(new UmbracoHelper(UmbracoContext.Current));
            //For<Examine.ExamineManager>().Use(Examine.ExamineManager.Instance);

            For<IUmbracoService>().Use<UmbracoService>();
            //For<ISearchService>().Use<SearchService>();

            //For<ITreeService>().Use<TreeService>();

            //For<IPositionRepository>().Use<PositionRepository>()/*.Named("PositionRepository")*/.LifecycleIs<HttpContextLifecycle>();
            //For<IPositionRepository>().Use(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject").PositionRepository).LifecycleIs<HttpContextLifecycle>();

            ////Manao Media service
            //For<IManaoMediaService>().Use<ManaoMediaService>();

            ////Cookies Alert Service
            //For<ICookiesAlertService>().Use<CookiesAlertService>();

            ////Google Analytics Service
            //For<IGoogleAnalyticsService>().Use<GoogleAnalyticsService>();
            //For<IGoogleAnalyticsService>().Use<GoogleAnalyticsService>();

            //////SEO Service
            ////For<ISEOService>().Use<SEOService>();

            //////Robots Service
            ////For<IRobotsService>().Use<RobotsService>();

            ////Configuration Service
            //For<IConfigurationService>().Use<ConfigurationService>();

            //Serializer Service
            //For<INodeModelSerializerService>().Use<NodeModelSerializerService>();
            //For<ILinkModelSerializerService>().Use<LinkModelSerializerService>();

            //Form Services
            For<IFieldService>().Use<FieldService>();
            For<IFormPluginService>().Use<FormPluginService>();
            For<IFormService>().Use<FormService>();
            For<IIntegrationService>().Use<IntegrationService>();
            For<ILogService>().Use<LogService>();
        }
        #endregion
    }
}
