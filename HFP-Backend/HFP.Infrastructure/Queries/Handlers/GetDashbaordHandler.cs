using HFP.Application.DTO;
using HFP.Application.Queries.Dashboard;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetDashbaordHandler : IQueryHandler<GetDashboardQuery, DashboardDto>
    {
        private readonly DbSet<ProductReadModel> _products;
        public GetDashbaordHandler(ReadDbContext context)
        {
            _products = context.Products;
        }

        public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var totalProducts = await _products.Where(pr => !pr.IsDeleted).CountAsync();

            return new DashboardDto()
            {
                TotalProducts = totalProducts,
                TotalTransactions = 0
            };
        }
    }
}
