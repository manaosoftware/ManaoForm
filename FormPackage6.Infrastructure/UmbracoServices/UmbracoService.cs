using FormPackage6.Core.Alias;
using FormPackage6.Core.Services.UmbracoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace FormPackage6.Infrastructure.UmbracoServices
{
    public class UmbracoService : IUmbracoService
    {
        private UmbracoHelper umbracoHelper;
        private ILocalizationService localizationService;
        private IDomainService domainService;
        public UmbracoService(
            UmbracoHelper umbracoHelper, 
            ILocalizationService _localizationService,
            IDomainService _domainService
            )
        {
            this.umbracoHelper = umbracoHelper;
            this.localizationService = _localizationService;
            this.domainService = _domainService;
        }
        public IPublishedContent GetNodeById(int id)
        {
            IPublishedContent content = umbracoHelper.TypedContent(id);

            return content;
        }

        public IPublishedContent GetContentByGuid(Guid id)
        {
            IPublishedContent content = umbracoHelper.TypedContent(id);
            
            return content;
        }

        public IMedia GetMediaByGuid(Guid id)
        {
            var service = ApplicationContext.Current.Services.MediaService;

            var media = service.GetById(id);

            return media;
        }

        public IContentType GetContentTypeById(int id)
        {
            IContentType content = ApplicationContext.Current.Services.ContentTypeService.GetContentType(id);

            return content;
        }

        public IPublishedContent GetHome(Guid contentId)
        {
            IPublishedContent content = umbracoHelper.TypedContent(contentId);
            return content.AncestorOrSelf(NodeAlias.Homepage);
        }
        public IPublishedContent GetConfiguration(Guid currentContentId, string configAlias)
        { 
            string configurationPropertyValue = string.Empty;
            IPublishedContent configurationNode = null;
            if (umbracoHelper.TypedContent(currentContentId) != null)
            {
                IPublishedContent content = umbracoHelper.Content(currentContentId);
                IPublishedContent homeNode = content.AncestorOrSelf(NodeAlias.Homepage);
                IPublishedContent configCollectionNode = homeNode.Children.Where(n => n.DocumentTypeAlias == NodeAlias.Configuration).FirstOrDefault();
                if (configCollectionNode != null)
                {
                    configurationNode = configCollectionNode.Children.Where(n => n.DocumentTypeAlias == configAlias).FirstOrDefault();
                }
            }
            return configurationNode;
        }
        public string GetDictionaryItem(string dictionaryItem, string defaultValue)
        {
            string output = string.Empty;
            var currentDomain = GetCurrentDomain();
            if (currentDomain != null)
            {
                try
                {
                    var dictionary = localizationService.GetDictionaryItemByKey(dictionaryItem);

                    if (dictionary != null)
                    {
                        var translated = dictionary.Translations.FirstOrDefault(x => x.Language.CultureInfo.ToString() == currentDomain.LanguageIsoCode);

                        output = translated != null ? translated.Value : string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error<string>("Error on As Dictionary Item : ", ex);
                }
            }

            if(string.IsNullOrEmpty(output))
            {
                output = defaultValue;
            }

            return output;
        }
        public IDomain GetCurrentDomain()
        {
            try
            {
                string currentHost = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                var domains = domainService.GetAll(false);
                if (domains != null && domains.Any())
                {
                    var currentDomain = domains.Where(n => (n.DomainName.EndsWith("/") ? n.DomainName.Substring(0, n.DomainName.LastIndexOf('/')) : n.DomainName) == currentHost).FirstOrDefault();
                    return currentDomain;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<string>("Error on Get Current Domain : ", ex);
            }
            return null; 
        }
    }
}
