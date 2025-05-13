using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class UserRole : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public User User { get; private set; }
        public Role Role { get; private set; }


        public UserRole(Guid userId, Guid roleId) : base(Guid.NewGuid())
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
