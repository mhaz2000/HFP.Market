using HFP.Domain.ValueObjects.Roles;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Role : AggregateRoot<Guid>
    {
        public RoleName Name { get; private set; }

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        public Role(Guid id, RoleName name) : base(id)
        {
            Name = name;
        }
    }

}
