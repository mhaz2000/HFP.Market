using HFP.Application.Services;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Services
{
    internal sealed class ConsumerReadService : IConsumerReadService
    {
        private readonly DbSet<ConsumerReadModel> _consumers;

        public ConsumerReadService(ReadDbContext context)
        {
            _consumers = context.Consumers;
        }
        public async Task<bool> CheckExistByNameAsync(string name)
        {
            return await _consumers.AnyAsync(t => t.Name.Trim() == name.Trim());
        }

        public async Task<bool> CheckExistByUIdAsync(string uid)
        {
            return await _consumers.AnyAsync(t => t.UId.Trim() == uid.Trim());

        }
    }
}
