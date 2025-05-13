using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Repositories.Base;

namespace HFP.Infrastructure.EF.Repositories
{
    internal sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(WriteDbContext context) : base(context)
        {
        }
    }
}
