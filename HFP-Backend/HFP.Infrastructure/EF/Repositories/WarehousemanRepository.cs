using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;

namespace HFP.Infrastructure.EF.Repositories
{
    internal class WarehousemanRepository : GenericRepository<Warehouseman>, IWarehousemanRepository
    {
        public WarehousemanRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
