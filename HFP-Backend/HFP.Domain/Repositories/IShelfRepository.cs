using HFP.Domain.Entities;
using HFP.Domain.Repositories.Base;

namespace HFP.Domain.Repositories
{
    public interface IShelfRepository : IGenericRepository<Shelf>
    {
        Task<IEnumerable<Shelf>> GetAllShelfAsync();
        void ClearShelves();
        Task AddBatchAsync(List<Shelf> shelves);
    }
}
