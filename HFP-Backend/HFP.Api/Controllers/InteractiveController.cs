using HFP.Api.Hubs;
using HFP.Application.Commands.Interactive;
using HFP.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFP.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InteractiveController : ControllerBase
    {
        private readonly IHubContext<SystemHub> _hubContext;
        private readonly ICommandDispatcher _commandDispatcher;

        public InteractiveController(IHubContext<SystemHub> hubContext, ICommandDispatcher commandDispatcher)
        {
            _hubContext = hubContext;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("CustomerEntered")]
        public async Task<IActionResult> CustomerEntered([FromBody] CustomerEnteredCommand command)
        {
            var isAdmin = await _commandDispatcher.DispatchAsync<CustomerEnteredCommand, bool>(command);

            await _hubContext.Clients.All.SendAsync("CardInserted", isAdmin);

            return Ok(new { message = "triggered." });
        }

        [HttpPost("WhichDoorToOpen/{doorCode}")]
        public async Task<IActionResult> WhichDoorToOpen([FromRoute] int doorCode)
        {
            await _commandDispatcher.DispatchAsync(new SendDoorCodeCommand(doorCode));
            return Ok();
        }

        [HttpPost("MarketDoorClosed")]
        public async Task<IActionResult> MarketDoorClosed()
        {

            await _hubContext.Clients.All.SendAsync("MarketDoorClosed");

            return Ok();
        }
    }
}
