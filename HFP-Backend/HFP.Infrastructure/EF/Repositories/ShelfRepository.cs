using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Repositories
{
    internal class ShelfRepository : GenericRepository<Shelf>, IShelfRepository
    {
        public ShelfRepository(WriteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shelf>> GetAllShelfAsync()
        {
            return await _context.Shelves.ToListAsync();
        }

        public void ClearShelves()
        {
            _context.RemoveRange(_context.Shelves);
        }

        public async Task AddBatchAsync(List<Shelf> shelves)
        {
            await _context.Shelves.AddRangeAsync(shelves);
            await _context.SaveChangesAsync();
        }
    }
}
