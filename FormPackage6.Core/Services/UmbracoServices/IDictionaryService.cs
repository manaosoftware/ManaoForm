using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Umbraco.Web.Dictionary;

namespace FormPackage6.Core.Services.UmbracoServices
{
    public interface IDictionaryService
    {
        IDictionary<string, string> GetDictionary(DefaultCultureDictionary defaultCultureDictionary, string rootKey);
        string GetDictionaryItem(ILocalizationService localizationService, CultureInfo language, string key);
    }
}
