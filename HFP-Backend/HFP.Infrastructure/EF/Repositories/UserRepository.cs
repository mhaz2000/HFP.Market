using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;

namespace HFP.Infrastructure.EF.Repositories
{
    internal sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
