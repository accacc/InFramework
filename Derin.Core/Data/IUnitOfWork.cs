using System;
using System.Data;
using System.Threading.Tasks;

namespace Derin.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }

        int SaveChanges(bool IsAuditEnabled = true);

        Task<int> SaveChangesAsync();
        

        //int SaveChanges(SaveOptions saveOptions, bool IsAuditEnabled=false);

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void RollBackTransaction();

        void CommitTransaction();
    }
}
