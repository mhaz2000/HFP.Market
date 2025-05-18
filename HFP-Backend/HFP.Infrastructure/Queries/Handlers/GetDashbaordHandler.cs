using HFP.Application.DTO;
using HFP.Application.Queries.Dashboard;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetDashbaordHandler : IQueryHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly DbSet<ProductReadModel> _products;
        private readonly DbSet<TransactionReadModel> _transactions;
        public GetDashbaordHandler(ReadDbContext context)
        {
            _products = context.Products;
            _transactions = context.Transactions;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var totalProducts = await _products.Where(pr => !pr.IsDeleted).CountAsync();
            var totalTransactions = await _transactions.CountAsync(t=> !t.IsDeleted && t.Type == TransactionType.Invoice);

            return new DashboardDto()
            {
                TotalProducts = totalProducts,
                TotalTransactions = totalTransactions
            };
        }
    }
}
