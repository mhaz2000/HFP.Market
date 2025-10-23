using System.Linq.Expressions;
using HFP.Domain.Entities;
using HFP.Domain.Repositories.Base;

namespace HFP.Domain.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task UpdateTransactionAsync(Transaction transaction);
        Task<Transaction?> GetWithInclueAsync(Expression<Func<Transaction, bool>> predicate);
        Task SetPendingTransactionAsCanceledAsync();
    }
}
