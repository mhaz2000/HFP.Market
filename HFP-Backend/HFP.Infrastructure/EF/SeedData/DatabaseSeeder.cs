using HFP.Domain.Entities;
using HFP.Domain.Factories;
using HFP.Domain.Factories.interfaces;
using HFP.Infrastructure.EF.Contexts;

namespace HFP.Infrastructure.EF.SeedData
{
    internal class DatabaseSeeder
    {
        private readonly WriteDbContext _context;
        private readonly IUserFactory _userFactory;

        public DatabaseSeeder(WriteDbContext context, IUserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any() && !_context.Roles.Any())
            {
                var adminRole = new Role(Guid.NewGuid(), "Admin");
                var userRole = new Role(Guid.NewGuid(), "User");

                var admin = _userFactory.Create("admin", "admin123");
                admin.AddRole(adminRole);

                _context.Roles.AddRange(adminRole, userRole);
                _context.Users.Add(admin);

                await _context.SaveChangesAsync();
            }
        }
    }
}
