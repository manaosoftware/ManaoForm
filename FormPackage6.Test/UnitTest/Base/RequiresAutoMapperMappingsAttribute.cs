using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Tests.UnitTest.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class RequiresAutoMapperMappingsAttribute : Attribute
    {
    }
}
