using HFP.Application.Services;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Services
{
    internal sealed class TransactionReadService : ITransactionReadService
    {
        private readonly DbSet<TransactionReadModel> _transactions;

        public TransactionReadService(ReadDbContext context)
        {
            _transactions = context.Transactions;
        }
        public async Task<decimal> GetTransactionAmountAsync(string buyerId)
        {
            var transaction = await _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(t => t.BuyerId == buyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("فاکتور یافت نشد.");

            return transaction.ProductTransactions.Sum(t => t.Product.Price * t.Quantity);
        }
    }
}
