using HFP.Domain.Consts;

namespace HFP.Infrastructure.EF.Models
{
    internal class UserReadModel
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<UserRoleReadModel> UserRoles { get; set; }
    }
}
