using HFP.Application.Services;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Services
{
    internal sealed class WarehousemanReadService : IWarehousemanReadService
    {
        private readonly DbSet<WarehousemanReadModel> _warehousemen;

        public WarehousemanReadService(ReadDbContext context)
        {
            _warehousemen = context.Warehousemen;
        }
        public async Task<bool> CheckExistByNameAsync(string name)
        {
            return await _warehousemen.AnyAsync(t => t.Name.Trim() == name.Trim());
        }

        public async Task<bool> CheckExistByUIdAsync(string uid)
        {
            return await _warehousemen.AnyAsync(t => t.UId.Trim() == uid.Trim());

        }

        public async Task<bool> CheckIsAdminAsync(string buyerId)
        {
            return await _warehousemen.AnyAsync(t => t.UId == buyerId);
        }
    }
}
