using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Tests.UnitTest.Base
{
    class AutoPublishedContentType : PublishedContentType
    {
        private static readonly PublishedPropertyType Default = new PublishedPropertyType("*", 0, "?");

        public AutoPublishedContentType(int id, string alias, IEnumerable<PublishedPropertyType> propertyTypes)
            : base(id, alias, Enumerable.Empty<string>(), propertyTypes)
        { }

        public AutoPublishedContentType(int id, string alias, IEnumerable<string> compositionAliases, IEnumerable<PublishedPropertyType> propertyTypes)
            : base(id, alias, compositionAliases, propertyTypes)
        { }

        public override PublishedPropertyType GetPropertyType(string alias)
        {
            var propertyType = base.GetPropertyType(alias);
            return propertyType ?? Default;
        }
    }
}
