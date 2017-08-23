using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace FormPackage6.Core.Extensions
{
    public static class PublishedContentExtension
    {
        public static string AsString(this IPublishedContent content, string alias, string defaultValue = "")
        {
            if (content.HasValue(alias))
            {
                defaultValue = content.GetPropertyValue<string>(alias);
            }
            return defaultValue;
        }
        public static int AsInt(this IPublishedContent content, string alias, int defaultValue = 0)
        {
            if (content.HasValue(alias))
            {
                int n;
                if (Int32.TryParse(content.AsString(alias), out n))
                {
                    defaultValue = n;
                }
            }
            return defaultValue;
        }
        public static bool AsBoolean(this IPublishedContent content, string alias)
        {
            bool booleanValue = false;
            if (content.HasValue(alias))
            {
                string value = content.AsString(alias).ToLower();
                booleanValue = value == "1" || value == "true";
            }
            return booleanValue;
        }
    }
}
