using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HFP.Application.DTO;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using HFP.Application.Commands.Users;
using HFP.Application.Queries.Users;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UsersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            return BaseOk();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<UserDto>>> Get([FromQuery] GetUsersQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var result = await _queryDispatcher.QueryAsync(new GetUserProfileQuery(UserId));
            return OkOrNotFound(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }
    }
}