using FormPackage6.Infrastructure.DAL.EF;
using FormPackage6.Infrastructure.Mock;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.Migrations
{
    public class Configuration : DbMigrationsConfiguration<EfUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EfUnitOfWork context)
        {
            InitializeIdentityForEF(context);
            context.Configuration.AutoDetectChangesEnabled = false;
            base.Seed(context);
            context.Configuration.AutoDetectChangesEnabled = true;
        }

        public static void InitializeIdentityForEF(EfUnitOfWork db)
        {
            IContainer container = StructureMapContainerInit.InitilizeMockContainer();
            MockData.Mock(container);
        }
    }
}
