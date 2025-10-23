using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;

namespace HFP.Infrastructure.EF.Repositories
{
    internal class ConsumerRepository : GenericRepository<Consumer>, IConsumerRepository
    {
        public ConsumerRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
