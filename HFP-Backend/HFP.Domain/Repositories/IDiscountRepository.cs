using HFP.Domain.Entities;
using HFP.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace HFP.Domain.Repositories
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<Discount?> GetWithInclueAsync(Expression<Func<Discount, bool>> predicate);

    }
}
