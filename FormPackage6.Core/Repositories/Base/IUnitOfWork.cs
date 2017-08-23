using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        //Database Database { get; }
        void BeginTransaction();
        void BeginTransactionWithoutTracking();
        void CommitTransaction();
        void RollbackTransaction();
        void Commit();
    }
}
