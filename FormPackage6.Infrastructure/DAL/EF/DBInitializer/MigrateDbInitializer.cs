using FormPackage6.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.DAL.EF.DBInitializer
{
    public class MigrateDbInitializer : MigrateDatabaseToLatestVersion<EfUnitOfWork, Configuration>
    {
    }
    public class MigrateDbInitializerCreateDatabaseOnly : CreateDatabaseIfNotExists<EfUnitOfWork>
    {

    }
}
