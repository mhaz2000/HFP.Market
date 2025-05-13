using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Roles;

namespace HFP.Domain.Factories.interfaces
{
    public interface IRoleFactory
    {
        Role Create(Guid id, RoleName roleName);
    }
}
