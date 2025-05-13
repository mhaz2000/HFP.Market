using HFP.Domain.ValueObjects.Users;
using HFP.Shared.Abstractions.Domain;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.Entities
{
    public class User : AggregateRoot<Guid>
    {
        public Username Username { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        public bool IsActive { get; private set; }

        private readonly List<UserRole> _roles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

        public User(Username username) : base()
        {
            Id = Guid.NewGuid();
            Username = username;
            IsActive = true;
        }

        public void SetPassword(PasswordHash passwordHash) 
            => PasswordHash = passwordHash;

        public void AddRole(Role role)
        {
            if (role == null)
                throw new BusinessException("نقش یافت نشد.");

            if (_roles.Any(r => r.RoleId == role.Id))
                throw new BusinessException($"نقش {role.Name} قبلا به کاربر {Username} تخصیص داده شده است.");

            _roles.Add(new UserRole(Id, role.Id));
        }

        public void RemoveRole(Role role)
        {
            var userRole = _roles.FirstOrDefault(r => r.RoleId == role.Id);
            if (userRole == null)
                throw new BusinessException($"نقش {role.Name} برای کاربر {Username} وجود ندارد.");

            _roles.Remove(userRole);
        }
    }

}
