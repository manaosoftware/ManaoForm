using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.PropertyEditors.ValueConverters;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.PublishedCache.XmlPublishedCache;

namespace Umbraco.Tests.UnitTest.Base
{
    public abstract class PublishedContentTestBase : BaseRoutingTest
    {
        public override void Initialize()
        {
            base.Initialize();

            // need to specify a custom callback for unit tests
            var propertyTypes = new[]
                {
                    // AutoPublishedContentType will auto-generate other properties
                    new PublishedPropertyType("content", 0, Constants.PropertyEditors.TinyMCEAlias),
                };
            var type = new AutoPublishedContentType(0, "anything", propertyTypes);
            PublishedContentType.GetPublishedContentTypeCallback = (alias) => type;

            var rCtx = GetRoutingContext("/test", 1234);
            UmbracoContext.Current = rCtx.UmbracoContext;

        }

        protected override void FreezeResolution()
        {
            if (PropertyValueConvertersResolver.HasCurrent == false)
                PropertyValueConvertersResolver.Current = new PropertyValueConvertersResolver(
                    new ActivatorServiceProvider(), Logger,
                    new[]
                        {
                            typeof(DatePickerValueConverter),
                            typeof(TinyMceValueConverter),
                            typeof(YesNoValueConverter)
                        });

            PublishedCachesResolver.Current = new PublishedCachesResolver(new PublishedCaches(
                new PublishedContentCache(), new PublishedMediaCache(ApplicationContext)));

            if (PublishedContentModelFactoryResolver.HasCurrent == false)
                PublishedContentModelFactoryResolver.Current = new PublishedContentModelFactoryResolver();

            base.FreezeResolution();
        }

        public override void TearDown()
        {
            base.TearDown();

            UmbracoContext.Current = null;
        }
    }
}
