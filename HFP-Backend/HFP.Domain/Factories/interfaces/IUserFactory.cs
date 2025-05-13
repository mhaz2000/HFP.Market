using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Users;

namespace HFP.Domain.Factories.interfaces
{
    public interface IUserFactory
    {
        User Create(Username username, string password);
    }
}
