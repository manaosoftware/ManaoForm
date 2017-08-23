using FormPackage6.Application.AutoMapperConfiguration.Base;
using StructureMap;

namespace FormPackage6.Application.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void Configure(IContainer container)
        {
            var mappers = container.GetAllInstances<IMapperBase>();
            foreach (var mapper in mappers)
                mapper.Configure(container);

        }
    }
}
