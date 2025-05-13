using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using HFP.Application.Queries.Users;

namespace HFP.Infrastructure.Queries.Handlers
{

    internal sealed class GetUserProfileHandler : IQueryHandler<GetUserProfileQuery, UserDto>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IMapper _mapper;

        public GetUserProfileHandler(ReadDbContext context, IMapper mapper)
        {
            _users = context.Users;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == query.userId);
            return _mapper.Map<UserDto>(user);
        }
    }


}
