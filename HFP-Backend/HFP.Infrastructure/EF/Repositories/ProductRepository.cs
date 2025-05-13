using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Repositories
{
    internal sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task<IList<Product>> GetByIdsAsync(List<Guid> ids)
            => await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
    }
}
