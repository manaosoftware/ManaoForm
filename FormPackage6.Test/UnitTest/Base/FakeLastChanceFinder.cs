using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Routing;

namespace Umbraco.Tests.UnitTest.Base
{
    internal class FakeLastChanceFinder : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            return false;
        }
    }
}
