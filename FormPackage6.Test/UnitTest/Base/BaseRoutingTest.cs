using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Umbraco.Tests.UnitTest.Base
{
    [TestFixture, RequiresSTA]
    public abstract class BaseRoutingTest : BaseWebTest
    {
        protected RoutingContext GetRoutingContext(string url, int templateId, RouteData routeData = null, bool setUmbracoContextCurrent = false, IUmbracoSettingsSection umbracoSettings = null)
        {
            if (umbracoSettings == null) umbracoSettings = SettingsForTests.GetDefault();

            var umbracoContext = GetUmbracoContext(url, templateId, routeData);
            var urlProvider = new UrlProvider(umbracoContext, umbracoSettings.WebRouting, new IUrlProvider[]
            {
                new DefaultUrlProvider(umbracoSettings.RequestHandler)
            });
            var routingContext = new RoutingContext(
                umbracoContext,
                Enumerable.Empty<IContentFinder>(),
                new FakeLastChanceFinder(),
                urlProvider);

            //assign the routing context back to the umbraco context
            umbracoContext.RoutingContext = routingContext;

            if (setUmbracoContextCurrent)
                UmbracoContext.Current = umbracoContext;

            return routingContext;
        }

        protected RoutingContext GetRoutingContext(string url, Template template, RouteData routeData = null)
        {
            return GetRoutingContext(url, template.Id, routeData);
        }

        protected RoutingContext GetRoutingContext(string url, RouteData routeData = null)
        {
            return GetRoutingContext(url, 1234, routeData);
        }
    }
}
