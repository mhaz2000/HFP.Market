using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HFP.Infrastructure.EF.Repositories
{
    internal class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(WriteDbContext context) : base(context)
        {
        }

        public Task<Discount?> GetWithInclueAsync(Expression<Func<Discount, bool>> predicate)
            => _context.Discounts.Include(c => c.Buyers).ThenInclude(c=> c.Buyer)
                .FirstOrDefaultAsync(predicate);
    }
}
