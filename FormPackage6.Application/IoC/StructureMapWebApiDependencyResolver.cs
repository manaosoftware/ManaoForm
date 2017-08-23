using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace FormPackage6.Application.IoC
{
    public class StructureMapWebApiDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        public StructureMapWebApiDependencyResolver(IContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            IContainer child = this.Container.GetNestedContainer();
            return new StructureMapWebApiDependencyResolver(child);
        }
    }
}
