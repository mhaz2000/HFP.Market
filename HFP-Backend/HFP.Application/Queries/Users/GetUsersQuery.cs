using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;

namespace HFP.Application.Queries.Users
{
    public record GetUsersQuery : PaginationQuery, IQuery<PaginatedResult<UserDto>>
    {
    }
}
