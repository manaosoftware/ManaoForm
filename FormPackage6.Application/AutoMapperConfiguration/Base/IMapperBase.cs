using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Application.AutoMapperConfiguration.Base
{
    public interface IMapperBase
    {
        void Configure(IContainer container);
    }
}
