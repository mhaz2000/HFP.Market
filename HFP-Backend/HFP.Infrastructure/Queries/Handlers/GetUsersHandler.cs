using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HFP.Application.DTO;
using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.ModuleExtensions;

using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Application.Queries.Users;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IMapper _mapper;

        public GetUsersHandler(ReadDbContext context, IMapper mapper)
        {
            _users = context.Users;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _users.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Username, $"%{query.Search}%"));

            var users = dbQuery.AsNoTracking();
            var paginatedResult = await users.ToPaginatedResultAsync<UserReadModel, UserDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
