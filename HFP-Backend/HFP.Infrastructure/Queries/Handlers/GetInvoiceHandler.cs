using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetInvoiceHandler : IQueryHandler<GetInvoiceQuery, IEnumerable<ProductTransactionDto>>
    {
        private readonly DbSet<TransactionReadModel> _transactions;
        public GetInvoiceHandler(ReadDbContext context)
        {
            _transactions = context.Transactions;
        }
        public async Task<IEnumerable<ProductTransactionDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("فاکتور یافت نشد.");

            return transaction.ProductTransactions.Select(s => new ProductTransactionDto()
            {
                ProductName = s.Product.Name,
                Price = s.Product.Price,
                Quantity = s.Quantity,
                ProductId = s.ProductId,
                ProductImage = s.Product.Image
            });

        }
    }
}
