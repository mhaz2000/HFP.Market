using Microsoft.EntityFrameworkCore;
using HFP.Application.Services;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.EF.Services
{
    internal sealed class RoleReadService : IRoleReadService
    {
        private readonly DbSet<RoleReadModel> _roles;

        public RoleReadService(ReadDbContext context)
            => _roles = context.Roles;

        public async Task<Guid> GetRoleIdByNameAsync(string roleName)
            => (await _roles.FirstOrDefaultAsync(c=> c.Name == roleName))!.Id;

        public async Task<string?> GetUserRoleNameAsync(Guid userId)
            => (await _roles.Include(c=>c.UserRoles).FirstOrDefaultAsync(r => r.UserRoles.Any(ur => ur.UserId == userId)))?.Name;
        

    }
}
