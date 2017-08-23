using FormPackage6.Core.Services;
using FormPackage6.Test.IoC;
using Moq;
using NUnit.Framework;
using StructureMap;
using System.Linq;
using System.Web;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Dictionary;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Tests.IoC;
using Umbraco.Tests.UnitTest.Base;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace FormPackage6.Test.UnitTest.Base
{
    [TestFixture]
    public abstract class UnitTestBase : BaseRoutingTest/*PublishedContentTestBase*/
    {
        protected IContainer IoCContainer { get; set; }

        protected UmbracoHelper umbracoHelper { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            IoCContainer = StructureMapContainerInit.InitilizeTestContainer();

            //FakeUmbracoContext();
            var appCtx = CreateApplicationContext();
            FakeUmbracoContext(appCtx);

            IoCContainer.Configure(x => x.For<UmbracoHelper>().Use(new UmbracoHelper(UmbracoContext.Current)));
            IoCContainer.Configure(x => x.For<Examine.ExamineManager>().Use(Examine.ExamineManager.Instance));

            AutoMapperConfiguration.Configure();
        }

        protected ServiceResult ServiceResultSuccess { get { return new ServiceResult(); } }
        protected Mock<Core.Repositories.Base.IUnitOfWork> UnitOfWorkMock { get { return new Mock<Core.Repositories.Base.IUnitOfWork>(); } }

        private void FakeUmbracoContext(ApplicationContext appCtx)
        {

            // Mock Umbraco Context
            var umbCtx = UmbracoContext.EnsureContext(
                Mock.Of<HttpContextBase>(),
                appCtx,
                new Mock<WebSecurity>(null, null).Object,
                Mock.Of<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(),
                true);

            // Mock UmbracoHelper
            var helper = new UmbracoHelper(umbCtx,
                Mock.Of<IPublishedContent>(),
                Mock.Of<ITypedPublishedContentQuery>(),
                Mock.Of<IDynamicPublishedContentQuery>(),
                Mock.Of<ITagQuery>(),
                Mock.Of<IDataTypeService>(),
                new UrlProvider(umbCtx, new[] { Mock.Of<IUrlProvider>() }, UrlProviderMode.Auto), Mock.Of<ICultureDictionary>(),
                Mock.Of<IUmbracoComponentRenderer>(),
                new MembershipHelper(umbCtx, Mock.Of<MembershipProvider>(), Mock.Of<RoleProvider>()));

            umbracoHelper = helper;
        }
    }
}
