using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Users
{
    public record GetUserProfileQuery(Guid userId) : IQuery<UserDto>;
}
