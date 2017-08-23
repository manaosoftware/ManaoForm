using FormPackage6.Core.Services.UmbracoServices;
using FormPackage6.Infrastructure.UmbracoServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Umbraco.Web.Dictionary;
using Umbraco.Web.WebApi;

namespace FormPackage6.Application.ApiControllers
{
    public class FormDictionaryApiController : UmbracoApiController
    {
        public IDictionary<string, string> GetDictionary(string locale, string key)
        {
            //Get all dictionary from root key by specific language.
            IDictionaryService dictionaryService = new DictionaryService();
            return dictionaryService.GetDictionary(new DefaultCultureDictionary(CultureInfo.GetCultureInfo(locale)), key);
        }
        public IDictionary<string, string> GetDictionary(string key)
        {
            //Get all dictionary from root key with no specific language.
            IDictionaryService dictionaryService = new DictionaryService();
            return dictionaryService.GetDictionary(new DefaultCultureDictionary(), key);
        }
        public string GetDictionaryItem(string locale, string key)
        {
            //Get single dictionary by specific language.
            ILocalizationService localizationService = Services.LocalizationService;
            IDictionaryService dictionaryService = new DictionaryService();
            return dictionaryService.GetDictionaryItem(localizationService, CultureInfo.GetCultureInfo(locale), key);
        }
    }
}
