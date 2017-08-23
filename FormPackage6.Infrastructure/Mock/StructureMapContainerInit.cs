using FormPackage6.Core.Repositories.Base;
using FormPackage6.Infrastructure.DAL.EF;
using StructureMap;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.Mock
{
    public class StructureMapContainerInit
    {
        public static IContainer InitilizeMockContainer()
        {
            var container = new Container(c => c.AddRegistry<MockRegistry>());
            return container;
        }
    }

    public class MockRegistry : Registry
    {
        #region Constructors and Destructors

        public MockRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    //scan.AssemblyContainingType<ICommand>();
                    //scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                    //scan.ConnectImplementationsToTypesClosing(typeof(IValidationHandler<>));
                    //scan.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>));
                });

            //For<ICommandHandler<SetPasswordCustomerCommand>>().Use<SetPasswordCommandHandler>();
            //For<ICommandHandler<SetPasswordMemberCommand>>().Use<SetPasswordCommandHandler>();

            //For(typeof(ICommandHandler<>)).DecorateAllWith(typeof(AuthorizationDecoratorCommandHandler<>));

            //For<IExcelConverter>();

            // Unit Of Work
            For<EfUnitOfWork>().Singleton().Use<EfUnitOfWork>().Named("UnitOfWorkObject");
            For<IUnitOfWork>().Singleton().Use<EfUnitOfWork>(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject"));

            // Asp.net Identity dependencys in managers
            For<DbContext>().Singleton().Use<EfUnitOfWork>(x => x.GetInstance<EfUnitOfWork>("UnitOfWorkObject"));

            // Repository
            //For<ITestRepository>().Singleton().Use<TestRepository>();
            //For<ITestRepositoryRead>().Singleton().Use<TestRepository>(x => (TestRepository) x.GetInstance<EfUnitOfWork>("UnitOfWorkObject").TestRepository);



        }
        #endregion
    }
}
