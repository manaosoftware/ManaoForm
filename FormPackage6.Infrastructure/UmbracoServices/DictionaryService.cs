using FormPackage6.Core.Services.UmbracoServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Umbraco.Web.Dictionary;

namespace FormPackage6.Infrastructure.UmbracoServices
{
    public class DictionaryService : IDictionaryService
    {
        public IDictionary<string, string> GetDictionary(DefaultCultureDictionary defaultCultureDictionary, string key)
        {
            //Get all dictionary that created under current key.
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            return GetDictionary(defaultCultureDictionary, key, ref dictionary);
        }
        public string GetDictionaryItem(ILocalizationService localizationService, CultureInfo language, string key)
        {
            string dictionaryValue = string.Empty;
            var found = localizationService.GetDictionaryItemByKey(key);
            if (found != null)
            {
                var byLang = found.Translations.FirstOrDefault(x => x.Language.IsoCode == language.IetfLanguageTag);
                if (byLang != null)
                {
                    dictionaryValue = byLang.Value;
                }
            }
            return dictionaryValue;
        }
        private IDictionary<string, string> GetDictionary(DefaultCultureDictionary defaultCultureDictionary, string key, ref IDictionary<string, string> dictionary)
        {
            foreach (var item in defaultCultureDictionary.GetChildren(key))
            {
                dictionary.Add(item);
                if (defaultCultureDictionary.GetChildren(item.Key).Any())
                {
                    GetDictionary(defaultCultureDictionary, item.Key, ref dictionary);
                }
            }
            return dictionary;
        }

    }
}
