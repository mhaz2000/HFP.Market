using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Queries;
using HFP.Application.Commands.Authentication;

namespace HFP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AuthenticationController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Post([FromBody] CredentialLoginCommand command)
        {
            var response = await _commandDispatcher.DispatchAsync<CredentialLoginCommand, string>(command);
            return OkOrNotFound(response);
        }

        [HttpGet("State")]
        public IActionResult State()
            => BaseOk("کاربر احراز هویت شده است.");
    }
}
