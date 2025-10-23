using System.Linq.Expressions;
using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Repositories
{
    internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(WriteDbContext context) : base(context)
        {
        }

        public Task<Transaction?> GetWithInclueAsync(Expression<Func<Transaction, bool>> predicate)
            => _context.Transactions.Include(c => c.Products).ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(predicate);

        public async Task SetPendingTransactionAsCanceledAsync()
        {
            await _context.Transactions
                .Where(t => t.Status == Domain.Consts.TransactionStatus.Pending)
                .ExecuteUpdateAsync(t => t.SetProperty(p => p.Status, Domain.Consts.TransactionStatus.Canceled));
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            foreach (var item in transaction.Products)
            {
                if (_context.ProductTransactions.Any(pr => pr.Id == item.Id))
                    _context.ProductTransactions.Update(item);
                else
                    await _context.ProductTransactions.AddAsync(item);
            }
            transaction.Date = DateTime.UtcNow;
            _context.Update(transaction);

            await _context.SaveChangesAsync();
        }
    }
}
