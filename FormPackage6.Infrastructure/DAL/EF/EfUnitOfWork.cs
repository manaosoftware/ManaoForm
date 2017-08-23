using FormPackage6.Core.DomainModel;
using FormPackage6.Core.Repositories;
using FormPackage6.Core.Repositories.Base;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.DAL.EF
{
    public class EfUnitOfWork : DbContext, IUnitOfWork
    {
        #region private members
        private IContainer container;
        private DbContextTransaction transaction;
        
        #endregion


        public EfUnitOfWork()
            : base("umbracoDbDSN")
        {

        }

        public EfUnitOfWork(IContainer container)
            : base("umbracoDbDSN")
        {
            this.container = container;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
        public void BeginTransaction()
        {
            // We are using transaction in the unit test project and this will conflict with this.
            if (this.Database.CurrentTransaction == null)
            {
                transaction = this.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            }
        }

        public void BeginTransactionWithoutTracking()
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.BeginTransaction();
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void CommitTransaction()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
                transaction.Rollback();
        }
    }
}
