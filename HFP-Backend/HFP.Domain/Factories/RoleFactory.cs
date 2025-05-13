using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Roles;

namespace HFP.Domain.Factories
{
    public class RoleFactory : IRoleFactory
    {
        public Role Create(Guid id, RoleName roleName)
        {
            var roleNameValue = RoleName.Create(roleName);
            return new Role(id, roleNameValue);
        }
    }
}
