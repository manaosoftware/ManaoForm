using FormPackage6.Application.AutoMapperConfiguration.Base;
using FormPackage6.Core.DomainModel.Base;
using FormPackage6.Core.Repositories.Base;
using FormPackage6.Core.Services.FormServices;
using FormPackage6.Core.Services.UmbracoServices;
using FormPackage6.Core.ValidationHandler.Base;
using FormPackage6.Dispatcher;
using FormPackage6.Infrastructure.DAL.EF;
using FormPackage6.Infrastructure.FormServices;
using FormPackage6.Infrastructure.UmbracoServices;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web.Pipeline;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace FormPackage6.Application.IoC
{
    public class StructureMapContainerInit
    {
        private static IContainer container;
        public static IContainer InitializeContainer()
        {
            if (container == null)
            {
                container = new Container(c => c.AddRegistry<DefaultRegistry>());

                string dllPath = System.Web.HttpContext.Current.Server.MapPath("/bin");
                container.Configure(c => c.Scan(scan => {
                    scan.AssembliesFromPath(dllPath);
                    scan.LookForRegistries();
                }));
            }

            return container;
        }
    }

    internal class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AssemblyContainingType<IDomainModel>();
                    scan.AddAllTypesOf<IDomainModel>();
                    scan.AddAllTypesOf<IMapperBase>();
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidationHandler<>));
                });

            Policies.FillAllPropertiesOfType<IContainer>();

            For<IValidateBus>().Use<DefaultValidateBus>();

            // Unit Of Work
            For<EfUnitOfWork>().Use<EfUnitOfWork>().Named("UnitOfWorkObject").LifecycleIs<HttpContextLifecycle>();
            For<IUnitOfWork>().Use(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject")).LifecycleIs<HttpContextLifecycle>();

            // Domain Services
            //For<ICustomerValidationService>().Use<CustomerValidationService>();

            //Umbraco Content Service
            For<UmbracoHelper>().Use(new UmbracoHelper(UmbracoContext.Current));
            For<Examine.ExamineManager>().Use(Examine.ExamineManager.Instance);

            For<IContentService>().Use(Umbraco.Core.ApplicationContext.Current.Services.ContentService);
            For<IContentTypeService>().Use(Umbraco.Core.ApplicationContext.Current.Services.ContentTypeService);
            For<ILocalizationService>().Use(ApplicationContext.Current.Services.LocalizationService);
            For<IDomainService>().Use(ApplicationContext.Current.Services.DomainService);
            
            For<IMediaService>().Use(Umbraco.Core.ApplicationContext.Current.Services.MediaService);

            For<IUmbracoService>().Use<UmbracoService>();
            //For<ISearchService>().Use<SearchService>();

            For<Core.Services.TreeService.ITreeService>().Use<Infrastructure.UmbracoServices.TreeService>();

            //For<IPositionRepository>().Use<PositionRepository>()/*.Named("PositionRepository")*/.LifecycleIs<HttpContextLifecycle>();
            //For<IPositionRepository>().Use(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject").PositionRepository).LifecycleIs<HttpContextLifecycle>();

            ////Manao Media service
            //For<IManaoMediaService>().Use<ManaoMediaService>();

            ////Cookies Alert Service
            //For<ICookiesAlertService>().Use<CookiesAlertService>();

            ////Google Analytics Service
            //For<IGoogleAnalyticsService>().Use<GoogleAnalyticsService>();

            ////SEO Service
            //For<ISEOService>().Use<SEOService>();

            ////Robots Service
            //For<IRobotsService>().Use<RobotsService>();

            ////Configuration Service
            //For<IConfigurationService>().Use<ConfigurationService>();

            ////Serializer Service
            //For<Core.Services.SerializerServices.INodeModelSerializerService>().Use<Infrastructure.SerializerServices.NodeModelSerializerService>();
            //For<Core.Services.SerializerServices.ILinkModelSerializerService>().Use<Infrastructure.SerializerServices.LinkModelSerializerService>();

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
