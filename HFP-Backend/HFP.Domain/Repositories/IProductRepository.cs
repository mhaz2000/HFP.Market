using HFP.Domain.Entities;
using HFP.Domain.Repositories.Base;

namespace HFP.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IList<Product>> GetByCodesAsync(List<string> codes);
        Task<IList<Product>> GetByIds(List<Guid> productIds);
    }
}
